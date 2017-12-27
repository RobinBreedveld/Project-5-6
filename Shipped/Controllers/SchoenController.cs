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
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly ApplicationDbContext _context;

        public SchoenController(ApplicationDbContext context, IEmailSender emailSender, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
            _emailSender = emailSender;

        }

        // GET: Schoen
        public async Task<IActionResult> Index(string searchString, string sortOrder, string merk, int min_prijs, int? max_prijs, string kleur, string type)
        {
            ViewBag.NaamSortParm = String.IsNullOrEmpty(sortOrder) ? "naam" : "";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.PrijsSortParm = sortOrder == "prijs" ? "prijs_desc" : "prijs";
            ViewBag.MerkSortParm = sortOrder == "merk" ? "merk_desc" : "merk";
            ViewBag.KleurSortParm = sortOrder == "kleur" ? "kleur_desc" : "kleur";
            ViewBag.AantalSortParm = sortOrder == "aantal" ? "aantal_desc" : "aantal";
            ViewBag.Aantal_gekochtSortParm = sortOrder == "aantal_gekocht" ? "aantal_gekocht_desc" : "aantal_gekocht";
            ViewBag.MaatSortParm = sortOrder == "maat" ? "maat_desc" : "maat";
            ViewBag.Alles = _context.Schoenen.GroupBy(p => new { p.Type, p.Merk, p.Kleur })
                            .Select(g => g.First())
                            .ToList();
            var schoenen = from a in _context.Schoenen.Include(d => d.Categorie) select a;
            //Als alles leeg is
            if (merk == null && min_prijs == 0 && max_prijs == null && kleur == null && type == null)
            {
                schoenen = from a in _context.Schoenen.Include(d => d.Categorie) select a;
            }
            //Als alles niet leeg is
            if (merk != null && min_prijs > 0 && max_prijs != null && kleur != null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Cases merk
            //Als merk en kleur niet leeg zijn
            if (merk != null && min_prijs == 0 && max_prijs == null && kleur != null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Kleur == kleur);
            }
            if (merk != null && min_prijs == 0 && max_prijs == null && kleur != null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Kleur == kleur && p.Type == type);
            }
            // Als merk niet leeg is
            if (merk != null && min_prijs == 0 && max_prijs == null && kleur == null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk);
            }

            if (merk != null && min_prijs == 0 && max_prijs == null && kleur == null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Type == type);
            }

            //Als merk en min_prijs niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs == null && kleur == null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Prijs >= min_prijs);
            }

            if (merk != null && min_prijs > 0 && max_prijs == null && kleur == null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Type == type);
            }

            //Als merk en min_prijs en kleur niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs == null && kleur != null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Kleur == kleur);
            }

            if (merk != null && min_prijs > 0 && max_prijs == null && kleur != null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Als merk en max_prijs niet leeg zijn
            if (merk != null && min_prijs == 0 && max_prijs != null && kleur == null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Prijs <= max_prijs);
            }

            if (merk != null && min_prijs == 0 && max_prijs != null && kleur == null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Prijs <= max_prijs && p.Type == type);
            }

            //Als merk en max_prijs en kleur niet leeg zijn
            if (merk != null && min_prijs == 0 && max_prijs != null && kleur != null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Prijs <= max_prijs && p.Kleur == kleur);
            }
            if (merk != null && min_prijs == 0 && max_prijs != null && kleur != null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Prijs <= max_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Als merk en min_prijs en max_prijs niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs != null && kleur == null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs);
            }
            if (merk != null && min_prijs > 0 && max_prijs != null && kleur == null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Type == type);
            }

            //Cases min_prijs & max_prijs
            //Als min_prijs niet leeg is
            if (merk == null && min_prijs > 0 && max_prijs == null && kleur == null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Prijs >= min_prijs);
            }
            if (merk == null && min_prijs > 0 && max_prijs == null && kleur == null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Prijs >= min_prijs && p.Type == type);
            }

            //Als min_prijs en kleur niet leeg zijn
            if (merk == null && min_prijs > 0 && max_prijs == null && kleur != null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Prijs >= min_prijs && p.Kleur == kleur);
            }
            if (merk == null && min_prijs > 0 && max_prijs == null && kleur != null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Prijs >= min_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Als max_prijs niet leeg is
            if (merk == null && min_prijs == 0 && max_prijs != null && kleur == null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Prijs <= max_prijs);
            }
            if (merk == null && min_prijs == 0 && max_prijs != null && kleur == null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Prijs <= max_prijs && p.Type == type);
            }

            //Als max_prijs en kleur niet leeg zijn
            if (merk == null && min_prijs == 0 && max_prijs != null && kleur != null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Prijs <= max_prijs && p.Kleur == kleur);
            }
            if (merk == null && min_prijs == 0 && max_prijs != null && kleur != null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Prijs <= max_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Als min_prijs en max_prijs niet leeg zijn
            if (merk == null && min_prijs > 0 && max_prijs != null && kleur == null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs);
            }
            if (merk == null && min_prijs > 0 && max_prijs != null && kleur == null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Type == type);
            }
            //Als min_prijs en max_prijs en kleur niet leeg zijn
            if (merk == null && min_prijs > 0 && max_prijs != null && kleur != null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Kleur == kleur);
            }
            if (merk == null && min_prijs > 0 && max_prijs != null && kleur != null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Cases kleur
            //Als kleur niet leeg is
            if (merk == null && min_prijs == 0 && max_prijs == null && kleur != null && type == null)
            {
                schoenen = _context.Schoenen.Where(p => p.Kleur == kleur);
            }
            if (merk == null && min_prijs == 0 && max_prijs == null && kleur != null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Kleur == kleur && p.Type == type);
            }
            if (merk == null && min_prijs == 0 && max_prijs == null && kleur == null && type != null)
            {
                schoenen = _context.Schoenen.Where(p => p.Type == type);
            }
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,Maat")] Schoen schoen)
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,Maat")] Schoen schoen)
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schoen = await _context.Schoenen.FirstOrDefaultAsync(m => m.Id == id);
            _context.Schoenen.Remove(schoen);
            HomeController controller = new HomeController(_context, _emailSender, _manager);

            await controller.DeleteAllFromShoppingCart(id, "Schoen");
            WishlistController wishlistcontroller = new WishlistController(_context);
            await wishlistcontroller.DeleteAllFromWishlist(id, "Schoen");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoenExists(int id)
        {
            return _context.Schoenen.Any(e => e.Id == id);
        }
    }
}
