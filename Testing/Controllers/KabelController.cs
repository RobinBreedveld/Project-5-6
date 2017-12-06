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
    public class KabelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KabelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kabel
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Kabels.Include(k => k.Categorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Kabel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kabel = await _context.Kabels
                .Include(k => k.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kabel == null)
            {
                return NotFound();
            }

            return View(kabel);
        }

        // GET: Kabel/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Kabel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,Lengte")] Kabel kabel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kabel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", kabel.CategorieId);
            return View(kabel);
        }

        // GET: Kabel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kabel = await _context.Kabels.SingleOrDefaultAsync(m => m.Id == id);
            if (kabel == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", kabel.CategorieId);
            return View(kabel);
        }

        // POST: Kabel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,Lengte")] Kabel kabel)
        {
            if (id != kabel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kabel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KabelExists(kabel.Id))
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
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", kabel.CategorieId);
            return View(kabel);
        }

        // GET: Kabel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kabel = await _context.Kabels
                .Include(k => k.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kabel == null)
            {
                return NotFound();
            }

            return View(kabel);
        }

        // POST: Kabel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kabel = await _context.Kabels.SingleOrDefaultAsync(m => m.Id == id);
            _context.Kabels.Remove(kabel);
            await _context.SaveChangesAsync();
            //deletes cartitem with same id as deleted item
            var delete = await _context.Cart.SingleOrDefaultAsync(m => m.Product_Id == id && m.Model_naam == "Kabel");
            if (delete != null){
            _context.Cart.Remove(delete);
            await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool KabelExists(int id)
        {
            return _context.Kabels.Any(e => e.Id == id);
        }
    }
}
