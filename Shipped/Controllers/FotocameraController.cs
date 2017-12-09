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
    public class FotocameraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FotocameraController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fotocamera
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewBag.NaamSortParm = String.IsNullOrEmpty(sortOrder) ? "naam" : "";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.PrijsSortParm = sortOrder == "prijs" ? "prijs_desc" : "prijs";
            ViewBag.MerkSortParm = sortOrder == "merk" ? "merk_desc" : "merk";
            ViewBag.KleurSortParm = sortOrder == "kleur" ? "kleur_desc" : "kleur";
            ViewBag.AantalSortParm = sortOrder == "aantal" ? "aantal_desc" : "aantal";
            ViewBag.Aantal_gekochtSortParm = sortOrder == "aantal_gekocht" ? "aantal_gekocht_desc" : "aantal_gekocht";
            ViewBag.MegaPixelsSortParm = sortOrder == "MegaPixels" ? "MegaPixels_desc" : "MegaPixels";
            ViewBag.FlitsSortParm = sortOrder == "Flits" ? "Flits_desc" : "Flits";
            ViewBag.Min_BereikSortParm = sortOrder == "Min_Bereik" ? "Min_Bereik_desc" : "Min_Bereik";
            ViewBag.Max_BereikSortParm = sortOrder == "Max_Bereik" ? "Max_Bereik_desc" : "Max_Bereik";
            
            var fotocameras = from a in _context.Fotocameras.Include(d => d.Categorie) select a;
           
            switch (sortOrder)
            {
                case "naam":
                    fotocameras = fotocameras.OrderByDescending(s => s.Naam);
                    break;
                case "type":
                    fotocameras = fotocameras.OrderBy(s => s.Type);
                    break;
                case "type_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Type);
                    break;
                case "prijs":
                    fotocameras = fotocameras.OrderBy(s => s.Prijs);
                    break;
                case "prijs_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Prijs);
                    break;   
                case "merk":
                    fotocameras = fotocameras.OrderBy(s => s.Merk);
                    break;
                case "merk_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Merk);
                    break;
                case "kleur":
                    fotocameras = fotocameras.OrderBy(s => s.Kleur);
                    break;
                case "kleur_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Kleur);
                    break;
                case "aantal":
                    fotocameras = fotocameras.OrderBy(s => s.Aantal);
                    break; 
                case "aantal_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Aantal);
                    break;
                case "aantal_gekocht":
                    fotocameras = fotocameras.OrderBy(s => s.Aantal_gekocht);
                    break; 
                case "aantal_gekocht_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Aantal_gekocht);
                    break;
                case "MegaPixels":
                    fotocameras = fotocameras.OrderBy(s => s.MegaPixels);
                    break; 
                case "MegaPixels_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.MegaPixels);
                    break; 
                case "Flits":
                    fotocameras = fotocameras.OrderBy(s => s.Flits);
                    break; 
                case "Flits_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Flits);
                    break; 
                case "Min_Bereik":
                    fotocameras = fotocameras.OrderBy(s => s.Min_Bereik);
                    break; 
                case "Min_Bereik_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Min_Bereik);
                    break;
                case "Max_Bereik":
                    fotocameras = fotocameras.OrderBy(s => s.Max_Bereik);
                    break; 
                case "Max_Bereik_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Max_Bereik);
                    break;         
                default:
                     fotocameras = fotocameras.OrderBy(s => s.Naam);
                    break;
            }

        return View(await fotocameras.ToListAsync());
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
         [Authorize(Roles="Admin")]
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
         [Authorize(Roles="Admin")]
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
         [Authorize(Roles="Admin")]
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
         [Authorize(Roles="Admin")]
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
         [Authorize(Roles="Admin")]
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
         [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotocamera = await _context.Fotocameras.FirstOrDefaultAsync(m => m.Id == id);
            _context.Fotocameras.Remove(fotocamera);            
            HomeController controller = new HomeController(_context);
            await controller.DeleteAllFromShoppingCart(id, "Fotocamera");
            await _context.SaveChangesAsync();           
            return RedirectToAction(nameof(Index));
        }

        private bool FotocameraExists(int id)
        {
            return _context.Fotocameras.Any(e => e.Id == id);
        }
    }
}