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
    public class SpelcomputerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpelcomputerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Spelcomputer
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Spelcomputers.Include(s => s.Categorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Spelcomputer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spelcomputer = await _context.Spelcomputers
                .Include(s => s.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (spelcomputer == null)
            {
                return NotFound();
            }

            return View(spelcomputer);
        }

        // GET: Spelcomputer/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Spelcomputer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,opslagcapaciteit,Opties")] Spelcomputer spelcomputer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spelcomputer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", spelcomputer.CategorieId);
            return View(spelcomputer);
        }

        // GET: Spelcomputer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spelcomputer = await _context.Spelcomputers.SingleOrDefaultAsync(m => m.Id == id);
            if (spelcomputer == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", spelcomputer.CategorieId);
            return View(spelcomputer);
        }

        // POST: Spelcomputer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,opslagcapaciteit,Opties")] Spelcomputer spelcomputer)
        {
            if (id != spelcomputer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spelcomputer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpelcomputerExists(spelcomputer.Id))
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
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", spelcomputer.CategorieId);
            return View(spelcomputer);
        }

        // GET: Spelcomputer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spelcomputer = await _context.Spelcomputers
                .Include(s => s.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (spelcomputer == null)
            {
                return NotFound();
            }

            return View(spelcomputer);
        }

        // POST: Spelcomputer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spelcomputer = await _context.Spelcomputers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Spelcomputers.Remove(spelcomputer);
            await _context.SaveChangesAsync();
            //deletes cartitem with same id as deleted item
            var delete = await _context.Cart.SingleOrDefaultAsync(m => m.Product_Id == id && m.Model_naam == "Spelcomputer");
            if (delete != null){
            _context.Cart.Remove(delete);
            await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SpelcomputerExists(int id)
        {
            return _context.Spelcomputers.Any(e => e.Id == id);
        }
    }
}
