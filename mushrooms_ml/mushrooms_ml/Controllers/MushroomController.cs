using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mushrooms_ml.Data;
using mushrooms_ml.Models;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Diagnostics;

namespace mushrooms_ml.Controllers
{
    public class MushroomController : Controller
    {
        private readonly MushroomDbContext dbContext;

        public MushroomController(MushroomDbContext context)
        {
            dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await dbContext.Mushrooms.ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create(bool fromAuction = false)
        {
            ViewBag.FromAuction = fromAuction;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Mushroom mushroom, bool fromAuction = false)
        {
            if (ModelState.IsValid)
            {
                dbContext.Mushrooms.Add(mushroom);
                await dbContext.SaveChangesAsync();
                if (fromAuction)
                {
                    var lot = new Lot
                    {
                        MushroomId = mushroom.Id,
                        Price = 0,
                        CreatedDate = DateTime.Now
                    };
                    dbContext.Lots.Add(lot);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("Index", "Auction");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mushroom);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await dbContext.Mushrooms.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Mushroom mushroom)
        {
            bool Exists(int x) => dbContext.Mushrooms.Any(e => e.Id == x);
            if (id != mushroom.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Entry(mushroom).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exists(mushroom.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(mushroom);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var mushroom = await dbContext.Mushrooms.FindAsync(id);
            if (mushroom != null)
            {
                dbContext.Mushrooms.Remove(mushroom);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Classify()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Classify(string capShape, string capSurface, string capColor, string bruises,
            string odor, string gillAttachment, string gillSpacing, string gillSize, string gillColor,
            string stalkShape, string stalkRoot, string stalkSurfaceAboveRing, string stalkSurfaceBelowRing,
            string stalkColorAboveRing, string stalkColorBelowRing, string veilType, string veilColor,
            string ringNumber, string ringType, string sporePrintColor, string population, string habitat)
        {
            if (ModelState.IsValid)
            {
                string inputData = capShape + "," + capSurface + "," + capColor + "," + bruises + "," + odor + "," +
                                   gillAttachment + "," + gillSpacing + "," + gillSize + "," + gillColor + "," +
                                   stalkShape + "," + stalkRoot + "," + stalkSurfaceAboveRing + "," +
                                   stalkSurfaceBelowRing + "," + stalkColorAboveRing + "," + stalkColorBelowRing + "," +
                                   veilType + "," + veilColor + "," + ringNumber + "," + ringType + "," +
                                   sporePrintColor + "," + population + "," + habitat;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "MushroomClassifier", "prediction_delegate.py");
                var psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = "\"" + path + "\" \"" + inputData + "\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                string result = "";
                using (var process = Process.Start(psi))
                {
                    using (var reader = process.StandardOutput)
                    {
                        result = reader.ReadToEnd();
                    }
                }
                ViewBag.PredictionResult = result;
            }
            return View();
        }
    }
}
