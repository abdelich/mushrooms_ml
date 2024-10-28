using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mushrooms_ml.Data;
using mushrooms_ml.Models;
using System.Diagnostics;

namespace mushrooms_ml.Controllers
{
    public class MushroomController : Controller
    {
        private readonly MushroomDbContext dbContext;

        public MushroomController(MushroomDbContext _context)
        {
            dbContext = _context;
        }

        public async Task<IActionResult> Index()
        {
            var mushroom = await dbContext.Mushrooms.ToListAsync();
            return View(mushroom);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Mushroom mushroom)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(mushroom);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mushroom);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var mushroom = await dbContext.Mushrooms.FindAsync(id);

            if (mushroom == null)
            {
                return NotFound();
            }

            return View(mushroom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Mushroom mushroom)
        {
            bool MushroomExists(int id)
            {
                return dbContext.Mushrooms.Any(e => e.Id == id);
            }

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
                    if (!MushroomExists(mushroom.Id))
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

        // Метод для GET запроса
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
                string inputData = $"{capShape},{capSurface},{capColor},{bruises},{odor},{gillAttachment}," +
                    $"{gillSpacing},{gillSize},{gillColor},{stalkShape},{stalkRoot},{stalkSurfaceAboveRing}," +
                    $"{stalkSurfaceBelowRing},{stalkColorAboveRing},{stalkColorBelowRing},{veilType}," +
                    $"{veilColor},{ringNumber},{ringType},{sporePrintColor},{population},{habitat}";

                string pythonScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "MushroomClassifier", "prediction_delegate.py");
                Debug.WriteLine(pythonScriptPath);
                Debug.WriteLine(inputData);

                var psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"\"{pythonScriptPath}\" \"{inputData}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                string prediction = "";
                using (var process = Process.Start(psi))
                {
                    using (var reader = process.StandardOutput)
                    {
                        prediction = reader.ReadToEnd();
                        Debug.WriteLine($"Prediction: {prediction}");
                        if (process.ExitCode != 0)
                        {
                            Debug.WriteLine(process.ExitCode);
                        }
                    }
                }
                ViewBag.PredictionResult = prediction;
            }

            return View();
        }
    }
}
