using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mushrooms_ml.Data;
using mushrooms_ml.Models;

namespace mushrooms_ml.Controllers
{
    public class AuctionController : Controller
    {
        private readonly MushroomDbContext dbContext;

        public AuctionController(MushroomDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int? currentUserId = HttpContext.Session.GetInt32("UserId");

            var lots = await dbContext.Lots
                .Include(l => l.Mushroom)
                .Include(l => l.User)
                .ToListAsync();

            ViewBag.CurrentUserId = currentUserId;
            return View(lots);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LotMushroomViewModel model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var mushroom = new Mushroom
            {
                CapShape = model.CapShape,
                CapSurface = model.CapSurface,
                CapColor = model.CapColor,
                Bruises = model.Bruises,
                Odor = model.Odor,
                GillAttachment = model.GillAttachment,
                GillSpacing = model.GillSpacing,
                GillSize = model.GillSize,
                GillColor = model.GillColor,
                StalkShape = model.StalkShape,
                StalkRoot = model.StalkRoot,
                StalkSurfaceAboveRing = model.StalkSurfaceAboveRing,
                StalkSurfaceBelowRing = model.StalkSurfaceBelowRing,
                StalkColorAboveRing = model.StalkColorAboveRing,
                StalkColorBelowRing = model.StalkColorBelowRing,
                VeilType = model.VeilType,
                VeilColor = model.VeilColor,
                RingNumber = model.RingNumber,
                RingType = model.RingType,
                SporePrintColor = model.SporePrintColor,
                Population = model.Population,
                Habitat = model.Habitat,
                Poisonous = model.Poisonous
            };
            dbContext.Mushrooms.Add(mushroom);
            await dbContext.SaveChangesAsync();
            var lot = new Lot
            {
                MushroomId = mushroom.Id,
                UserId = userId.Value,
                Price = model.Price,
                CreatedDate = DateTime.Now
            };
            dbContext.Lots.Add(lot);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var lot = await dbContext.Lots
                .Include(l => l.Mushroom)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lot == null)
            {
                return NotFound();
            }

            if (lot.UserId != userId.Value)
            {
                return Forbid();
            }

            dbContext.Lots.Remove(lot);
            dbContext.Mushrooms.Remove(lot.Mushroom);

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var lot = await dbContext.Lots
                .Include(x => x.Mushroom)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (lot == null)
            {
                return NotFound();
            }
            return View(lot.Mushroom);
        }
    }
}
