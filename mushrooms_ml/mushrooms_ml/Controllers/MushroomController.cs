﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mushrooms_ml.Data;
using mushrooms_ml.Models;

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
    }
}
