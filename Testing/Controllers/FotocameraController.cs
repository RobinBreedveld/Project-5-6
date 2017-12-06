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
    public class FotocameraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FotocameraController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fotocamera
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Fotocameras.Include(f => f.Categorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Fotocamera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotocamera = await _context.Fotocameras
                .Include(f => f.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fotocamera == null)
            {
                return NotFound();
            }

            return View(fotocamera);
        }

        // GET: Fotocamera/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Fotocamera/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,MegaPixels,Flits,Min_Bereik,Max_Bereik")] Fotocamera fotocamera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fotocamera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", fotocamera.CategorieId);
            return View(fotocamera);
        }

        // GET: Fotocamera/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotocamera = await _context.Fotocameras.SingleOrDefaultAsync(m => m.Id == id);
            if (fotocamera == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", fotocamera.CategorieId);
            return View(fotocamera);
        }

        // POST: Fotocamera/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,MegaPixels,Flits,Min_Bereik,Max_Bereik")] Fotocamera fotocamera)
        {
            if (id != fotocamera.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fotocamera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotocameraExists(fotocamera.Id))
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
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", fotocamera.CategorieId);
            return View(fotocamera);
        }

        // GET: Fotocamera/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotocamera = await _context.Fotocameras
                .Include(f => f.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fotocamera == null)
            {
                return NotFound();
            }

            return View(fotocamera);
        }

        // POST: Fotocamera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotocamera = await _context.Fotocameras.SingleOrDefaultAsync(m => m.Id == id);
            _context.Fotocameras.Remove(fotocamera);
            await _context.SaveChangesAsync();
            //deletes cartitem with same id as deleted item
            var delete = await _context.Cart.SingleOrDefaultAsync(m => m.Product_Id == id && m.Model_naam == "Fotocamera");
            if (delete != null){
            _context.Cart.Remove(delete);
            await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FotocameraExists(int id)
        {
            return _context.Fotocameras.Any(e => e.Id == id);
        }
    }
}
