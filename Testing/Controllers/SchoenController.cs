using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using login2.Data;
using login2.Models;

namespace login2.Controllers
{
    public class SchoenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Schoen
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Schoenen.Include(s => s.Categorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Schoen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoen = await _context.Schoenen
                .Include(s => s.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (schoen == null)
            {
                return NotFound();
            }

            return View(schoen);
        }

        // GET: Schoen/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Schoen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,Maat,Materiaal,Geslacht")] Schoen schoen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schoen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", schoen.CategorieId);
            return View(schoen);
        }

        // GET: Schoen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoen = await _context.Schoenen.SingleOrDefaultAsync(m => m.Id == id);
            if (schoen == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", schoen.CategorieId);
            return View(schoen);
        }

        // POST: Schoen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,Maat,Materiaal,Geslacht")] Schoen schoen)
        {
            if (id != schoen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoenExists(schoen.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", schoen.CategorieId);
            return View(schoen);
        }

        // GET: Schoen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoen = await _context.Schoenen
                .Include(s => s.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (schoen == null)
            {
                return NotFound();
            }

            return View(schoen);
        }

        // POST: Schoen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schoen = await _context.Schoenen.SingleOrDefaultAsync(m => m.Id == id);
            _context.Schoenen.Remove(schoen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoenExists(int id)
        {
            return _context.Schoenen.Any(e => e.Id == id);
        }
    }
}
