using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mushrooms_ml.Data;
using mushrooms_ml.Models;
using System.Linq;
using System.Threading.Tasks;

namespace mushrooms_ml.Controllers
{
    public class AuctionController : Controller
    {
        private readonly MushroomDbContext _context;

        public AuctionController(MushroomDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var auctions = await _context.AuctionItems
                .Include(a => a.Mushroom)
                .Where(a => !a.IsSold && a.EndTime > DateTime.UtcNow)
                .ToListAsync();

            return View(auctions);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mushroom mushroom)
        {
            if (ModelState.IsValid)
            {
                mushroom.IsForSale = true;
                mushroom.CreatedAt = DateTime.UtcNow;
                _context.Mushrooms.Add(mushroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mushroom);
        }
        public async Task<IActionResult> View(int id)
        {
            var mushroom = await _context.Mushrooms.FindAsync(id);
            if (mushroom == null || !mushroom.IsForSale)
            {
                return NotFound();
            }
            return View(mushroom);
        }
        public async Task<IActionResult> Buy(int id)
        {
            var mushroom = await _context.Mushrooms.FindAsync(id);
            if (mushroom == null || !mushroom.IsForSale)
            {
                return NotFound();
            }
            return View(mushroom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyConfirmed(int id)
        {
            var auctionItem = await _context.AuctionItems
                .Include(a => a.Mushroom)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (auctionItem == null || auctionItem.IsSold)
            {
                return NotFound();
            }

            auctionItem.IsSold = true;

            auctionItem.BuyerId = 1;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}