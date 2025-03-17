using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mushrooms_ml.Data;
using mushrooms_ml.Models;
using System.Threading.Tasks;

namespace mushrooms_ml.Controllers
{
    public class UserController : Controller
    {
        private readonly MushroomDbContext dbContext;

        public UserController(MushroomDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool loginExists = await dbContext.Users.AnyAsync(u => u.Login == model.Login);
            if (loginExists)
            {
                ModelState.AddModelError("", "This login is already in use.");
                return View(model);
            }

            bool emailExists = await dbContext.Users.AnyAsync(u => u.Email == model.Email);
            if (emailExists)
            {
                ModelState.AddModelError("", "This email is already in use.");
                return View(model);
            }

            dbContext.Users.Add(model);
            await dbContext.SaveChangesAsync();

            HttpContext.Session.SetInt32("UserId", model.Id);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Login == login);

            if (user == null || user.Password != password)
            {
                ModelState.AddModelError("", "Password is incorrect.");
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index", "Home");
        }
    }
}
