using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using login2.Data;
using login2.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using login2.Models.AccountViewModels;
using login2.Services;

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
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewBag.NaamSortParm = String.IsNullOrEmpty(sortOrder) ? "naam" : "";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.PrijsSortParm = sortOrder == "prijs" ? "prijs_desc" : "prijs";
            ViewBag.MerkSortParm = sortOrder == "merk" ? "merk_desc" : "merk";
            ViewBag.KleurSortParm = sortOrder == "kleur" ? "kleur_desc" : "kleur";
            ViewBag.AantalSortParm = sortOrder == "aantal" ? "aantal_desc" : "aantal";
            ViewBag.Aantal_gekochtSortParm = sortOrder == "aantal_gekocht" ? "aantal_gekocht_desc" : "aantal_gekocht";
            ViewBag.MaatSortParm = sortOrder == "maat" ? "maat_desc" : "maat";
            ViewBag.MateriaalSortParm = sortOrder == "materiaal" ? "materiaal_desc" : "materiaal";
            ViewBag.GeslachtSortParm = sortOrder == "geslacht" ? "geslacht_desc" : "geslacht";
            
            var schoenen = from a in _context.Schoenen.Include(d => d.Categorie) select a;

            switch (sortOrder)
            {
                case "naam":
                    schoenen = schoenen.OrderByDescending(s => s.Naam);
                    break;
                case "type":
                    schoenen = schoenen.OrderBy(s => s.Type);
                    break;
                case "type_desc":
                    schoenen = schoenen.OrderByDescending(s => s.Type);
                    break;
                case "prijs":
                    schoenen = schoenen.OrderBy(s => s.Prijs);
                    break;
                case "prijs_desc":
                    schoenen = schoenen.OrderByDescending(s => s.Prijs);
                    break;   
                case "merk":
                    schoenen = schoenen.OrderBy(s => s.Merk);
                    break;
                case "merk_desc":
                    schoenen = schoenen.OrderByDescending(s => s.Merk);
                    break;
                case "kleur":
                    schoenen = schoenen.OrderBy(s => s.Kleur);
                    break;
                case "kleur_desc":
                    schoenen = schoenen.OrderByDescending(s => s.Kleur);
                    break;
                case "aantal":
                    schoenen = schoenen.OrderBy(s => s.Aantal);
                    break; 
                case "aantal_desc":
                    schoenen = schoenen.OrderByDescending(s => s.Aantal);
                    break;
                case "aantal_gekocht":
                    schoenen = schoenen.OrderBy(s => s.Aantal_gekocht);
                    break; 
                case "aantal_gekocht_desc":
                    schoenen = schoenen.OrderByDescending(s => s.Aantal_gekocht);
                    break;
                case "maat":
                    schoenen = schoenen.OrderBy(s => s.Maat);
                    break; 
                case "maat_desc":
                    schoenen = schoenen.OrderByDescending(s => s.Maat);
                    break; 
                case "materiaal":
                    schoenen = schoenen.OrderBy(s => s.Materiaal);
                    break; 
                case "materiaal_desc":
                    schoenen = schoenen.OrderByDescending(s => s.Materiaal);
                    break;
                case "geslacht":
                    schoenen = schoenen.OrderBy(s => s.Geslacht);
                    break;
                case "geslacht_desc":
                    schoenen = schoenen.OrderByDescending(s => s.Geslacht);
                    break;         
                default:
                     schoenen = schoenen.OrderBy(s => s.Naam);
                    break;
            }
        
            return View(await schoenen.ToListAsync());
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
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schoen = await _context.Schoenen.FirstOrDefaultAsync(m => m.Id == id);
            _context.Schoenen.Remove(schoen);
            HomeController controller = new HomeController(_context);
            await controller.DeleteAllFromShoppingCart(id, "Schoen");
            await _context.SaveChangesAsync();     
            return RedirectToAction(nameof(Index));
        }

        private bool SchoenExists(int id)
        {
            return _context.Schoenen.Any(e => e.Id == id);
        }
    }
}
