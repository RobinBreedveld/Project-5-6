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
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewBag.NaamSortParm = String.IsNullOrEmpty(sortOrder) ? "naam" : "";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.PrijsSortParm = sortOrder == "prijs" ? "prijs_desc" : "prijs";
            ViewBag.MerkSortParm = sortOrder == "merk" ? "merk_desc" : "merk";
            ViewBag.KleurSortParm = sortOrder == "kleur" ? "kleur_desc" : "kleur";
            ViewBag.AantalSortParm = sortOrder == "aantal" ? "aantal_desc" : "aantal";
            ViewBag.Aantal_gekochtSortParm = sortOrder == "aantal_gekocht" ? "aantal_gekocht_desc" : "aantal_gekocht";
            ViewBag.OpslagcapaciteitSortParm = sortOrder == "opslagcapaciteit" ? "opslagcapaciteit_desc" : "opslagcapaciteit";
            ViewBag.OptiesSortParm = sortOrder == "opties" ? "opties_desc" : "opties";
            
            var spelcomputers = from a in _context.Spelcomputers.Include(d => d.Categorie) select a;

            switch (sortOrder)
            {
                case "naam":
                    spelcomputers = spelcomputers.OrderByDescending(s => s.Naam);
                    break;
                case "type":
                    spelcomputers = spelcomputers.OrderBy(s => s.Type);
                    break;
                case "type_desc":
                    spelcomputers = spelcomputers.OrderByDescending(s => s.Type);
                    break;
                case "prijs":
                    spelcomputers = spelcomputers.OrderBy(s => s.Prijs);
                    break;
                case "prijs_desc":
                    spelcomputers = spelcomputers.OrderByDescending(s => s.Prijs);
                    break;   
                case "merk":
                    spelcomputers = spelcomputers.OrderBy(s => s.Merk);
                    break;
                case "merk_desc":
                    spelcomputers = spelcomputers.OrderByDescending(s => s.Merk);
                    break;
                case "kleur":
                    spelcomputers = spelcomputers.OrderBy(s => s.Kleur);
                    break;
                case "kleur_desc":
                    spelcomputers = spelcomputers.OrderByDescending(s => s.Kleur);
                    break;
                case "aantal":
                    spelcomputers = spelcomputers.OrderBy(s => s.Aantal);
                    break; 
                case "aantal_desc":
                    spelcomputers = spelcomputers.OrderByDescending(s => s.Aantal);
                    break;
                case "aantal_gekocht":
                    spelcomputers = spelcomputers.OrderBy(s => s.Aantal_gekocht);
                    break; 
                case "aantal_gekocht_desc":
                    spelcomputers = spelcomputers.OrderByDescending(s => s.Aantal_gekocht);
                    break;
                case "opslagcapaciteit":
                    spelcomputers = spelcomputers.OrderBy(s => s.Opslagcapaciteit);
                    break; 
                case "opslagcapaciteit_desc":
                    spelcomputers = spelcomputers.OrderByDescending(s => s.Opslagcapaciteit);
                    break; 
                case "opties":
                    spelcomputers = spelcomputers.OrderBy(s => s.Opties);
                    break; 
                case "opties_desc":
                    spelcomputers = spelcomputers.OrderByDescending(s => s.Opties);
                    break;     
                default:
                     spelcomputers = spelcomputers.OrderBy(s => s.Naam);
                    break;
            }
        
            return View(await spelcomputers.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,Opslagcapaciteit,Opties")] Spelcomputer spelcomputer)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,Opslagcapaciteit,Opties")] Spelcomputer spelcomputer)
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
