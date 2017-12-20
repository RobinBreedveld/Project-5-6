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
    public class HorlogeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly ApplicationDbContext _context;

        public HorlogeController(ApplicationDbContext context, IEmailSender emailSender, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
            _emailSender = emailSender;

        }

        // GET: Horloge
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewBag.NaamSortParm = String.IsNullOrEmpty(sortOrder) ? "naam" : "";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.PrijsSortParm = sortOrder == "prijs" ? "prijs_desc" : "prijs";
            ViewBag.MerkSortParm = sortOrder == "merk" ? "merk_desc" : "merk";
            ViewBag.KleurSortParm = sortOrder == "kleur" ? "kleur_desc" : "kleur";
            ViewBag.AantalSortParm = sortOrder == "aantal" ? "aantal_desc" : "aantal";
            ViewBag.Aantal_gekochtSortParm = sortOrder == "aantal_gekocht" ? "aantal_gekocht_desc" : "aantal_gekocht";
            ViewBag.GrootteSortParm = sortOrder == "grootte" ? "grootte_desc" : "grootte";
            ViewBag.MateriaalSortParm = sortOrder == "materiaal" ? "materiaal_desc" : "materiaal";
            ViewBag.GeslachtSortParm = sortOrder == "geslacht" ? "geslacht_desc" : "geslacht";

            var horloges = from a in _context.Horloges.Include(d => d.Categorie) select a;

            switch (sortOrder)
            {
                case "naam":
                    horloges = horloges.OrderByDescending(s => s.Naam);
                    break;
                case "type":
                    horloges = horloges.OrderBy(s => s.Type);
                    break;
                case "type_desc":
                    horloges = horloges.OrderByDescending(s => s.Type);
                    break;
                case "prijs":
                    horloges = horloges.OrderBy(s => s.Prijs);
                    break;
                case "prijs_desc":
                    horloges = horloges.OrderByDescending(s => s.Prijs);
                    break;
                case "merk":
                    horloges = horloges.OrderBy(s => s.Merk);
                    break;
                case "merk_desc":
                    horloges = horloges.OrderByDescending(s => s.Merk);
                    break;
                case "kleur":
                    horloges = horloges.OrderBy(s => s.Kleur);
                    break;
                case "kleur_desc":
                    horloges = horloges.OrderByDescending(s => s.Kleur);
                    break;
                case "aantal":
                    horloges = horloges.OrderBy(s => s.Aantal);
                    break;
                case "aantal_desc":
                    horloges = horloges.OrderByDescending(s => s.Aantal);
                    break;
                case "aantal_gekocht":
                    horloges = horloges.OrderBy(s => s.Aantal_gekocht);
                    break;
                case "aantal_gekocht_desc":
                    horloges = horloges.OrderByDescending(s => s.Aantal_gekocht);
                    break;
                case "grootte":
                    horloges = horloges.OrderBy(s => s.Grootte);
                    break;
                case "grootte_desc":
                    horloges = horloges.OrderByDescending(s => s.Grootte);
                    break;
                case "materiaal":
                    horloges = horloges.OrderBy(s => s.Materiaal);
                    break;
                case "materiaal_desc":
                    horloges = horloges.OrderByDescending(s => s.Materiaal);
                    break;
                case "geslacht":
                    horloges = horloges.OrderBy(s => s.Geslacht);
                    break;
                case "geslacht_desc":
                    horloges = horloges.OrderByDescending(s => s.Geslacht);
                    break;
                default:
                    horloges = horloges.OrderBy(s => s.Naam);
                    break;
            }

            return View(await horloges.ToListAsync());
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horloge = await _context.Horloges.FirstOrDefaultAsync(m => m.Id == id);
            _context.Horloges.Remove(horloge);
            HomeController controller = new HomeController(_context, _emailSender, _manager);

            await controller.DeleteAllFromShoppingCart(id, "Horloge");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorlogeExists(int id)
        {
            return _context.Horloges.Any(e => e.Id == id);
        }
    }
}
