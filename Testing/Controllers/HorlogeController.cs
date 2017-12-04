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
    public class HorlogeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HorlogeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Horloge
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Horloges.Include(h => h.Categorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Horloge/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horloge = await _context.Horloges
                .Include(h => h.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (horloge == null)
            {
                return NotFound();
            }

            return View(horloge);
        }

        // GET: Horloge/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Horloge/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,Grootte,Materiaal,Geslacht")] Horloge horloge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horloge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", horloge.CategorieId);
            return View(horloge);
        }

        // GET: Horloge/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horloge = await _context.Horloges.SingleOrDefaultAsync(m => m.Id == id);
            if (horloge == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", horloge.CategorieId);
            return View(horloge);
        }

        // POST: Horloge/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,Grootte,Materiaal,Geslacht")] Horloge horloge)
        {
            if (id != horloge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horloge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorlogeExists(horloge.Id))
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
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", horloge.CategorieId);
            return View(horloge);
        }

        // GET: Horloge/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horloge = await _context.Horloges
                .Include(h => h.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (horloge == null)
            {
                return NotFound();
            }

            return View(horloge);
        }

        // POST: Horloge/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horloge = await _context.Horloges.SingleOrDefaultAsync(m => m.Id == id);
            _context.Horloges.Remove(horloge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorlogeExists(int id)
        {
            return _context.Horloges.Any(e => e.Id == id);
        }
    }
}
