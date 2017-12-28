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
        public async Task<IActionResult> Index(string searchString, string sortOrder, string merk, int min_prijs, int? max_prijs, string kleur, string type)
        {
            ViewBag.NaamSortParm = String.IsNullOrEmpty(sortOrder) ? "naam" : "";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.PrijsSortParm = sortOrder == "prijs" ? "prijs_desc" : "prijs";
            ViewBag.MerkSortParm = sortOrder == "merk" ? "merk_desc" : "merk";
            ViewBag.KleurSortParm = sortOrder == "kleur" ? "kleur_desc" : "kleur";
            ViewBag.AantalSortParm = sortOrder == "aantal" ? "aantal_desc" : "aantal";
            ViewBag.Aantal_gekochtSortParm = sortOrder == "aantal_gekocht" ? "aantal_gekocht_desc" : "aantal_gekocht";
            ViewBag.AllesTypes = _context.Horloges.GroupBy(p => new { p.Type})
                            .Select(g => g.First())
                            .ToList();
            ViewBag.AllesMerken = _context.Horloges.GroupBy(p => new {p.Merk})
                            .Select(g => g.First())
                            .ToList();
             ViewBag.AllesKleuren = _context.Horloges.GroupBy(p => new {p.Kleur })
                            .Select(g => g.First())
                            .ToList();
            var horloges = from a in _context.Horloges.Include(d => d.Categorie) select a;
            //Als alles leeg is
            if (merk == null && min_prijs == 0 && max_prijs == null && kleur == null && type == null)
            {
                horloges = from a in _context.Horloges.Include(d => d.Categorie) select a;
            }
            //Als alles niet leeg is
            if (merk != null && min_prijs > 0 && max_prijs != null && kleur != null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Cases merk
            //Als merk en kleur niet leeg zijn
            if (merk != null && min_prijs == 0 && max_prijs == null && kleur != null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Kleur == kleur);
            }
            if (merk != null && min_prijs == 0 && max_prijs == null && kleur != null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Kleur == kleur && p.Type == type);
            }
            // Als merk niet leeg is
            if (merk != null && min_prijs == 0 && max_prijs == null && kleur == null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk);
            }

            if (merk != null && min_prijs == 0 && max_prijs == null && kleur == null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Type == type);
            }

            //Als merk en min_prijs niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs == null && kleur == null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Prijs >= min_prijs);
            }

            if (merk != null && min_prijs > 0 && max_prijs == null && kleur == null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Type == type);
            }

            //Als merk en min_prijs en kleur niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs == null && kleur != null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Kleur == kleur);
            }

            if (merk != null && min_prijs > 0 && max_prijs == null && kleur != null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Als merk en max_prijs niet leeg zijn
            if (merk != null && min_prijs == 0 && max_prijs != null && kleur == null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Prijs <= max_prijs);
            }

            if (merk != null && min_prijs == 0 && max_prijs != null && kleur == null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Prijs <= max_prijs && p.Type == type);
            }

            //Als merk en max_prijs en kleur niet leeg zijn
            if (merk != null && min_prijs == 0 && max_prijs != null && kleur != null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Prijs <= max_prijs && p.Kleur == kleur);
            }
            if (merk != null && min_prijs == 0 && max_prijs != null && kleur != null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Prijs <= max_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Als merk en min_prijs en max_prijs niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs != null && kleur == null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs);
            }
            if (merk != null && min_prijs > 0 && max_prijs != null && kleur == null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Type == type);
            }

            //Cases min_prijs & max_prijs
            //Als min_prijs niet leeg is
            if (merk == null && min_prijs > 0 && max_prijs == null && kleur == null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Prijs >= min_prijs);
            }
            if (merk == null && min_prijs > 0 && max_prijs == null && kleur == null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Prijs >= min_prijs && p.Type == type);
            }

            //Als min_prijs en kleur niet leeg zijn
            if (merk == null && min_prijs > 0 && max_prijs == null && kleur != null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Prijs >= min_prijs && p.Kleur == kleur);
            }
            if (merk == null && min_prijs > 0 && max_prijs == null && kleur != null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Prijs >= min_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Als max_prijs niet leeg is
            if (merk == null && min_prijs == 0 && max_prijs != null && kleur == null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Prijs <= max_prijs);
            }
            if (merk == null && min_prijs == 0 && max_prijs != null && kleur == null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Prijs <= max_prijs && p.Type == type);
            }

            //Als max_prijs en kleur niet leeg zijn
            if (merk == null && min_prijs == 0 && max_prijs != null && kleur != null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Prijs <= max_prijs && p.Kleur == kleur);
            }
            if (merk == null && min_prijs == 0 && max_prijs != null && kleur != null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Prijs <= max_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Als min_prijs en max_prijs niet leeg zijn
            if (merk == null && min_prijs > 0 && max_prijs != null && kleur == null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs);
            }
            if (merk == null && min_prijs > 0 && max_prijs != null && kleur == null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Type == type);
            }
            //Als min_prijs en max_prijs en kleur niet leeg zijn
            if (merk == null && min_prijs > 0 && max_prijs != null && kleur != null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Kleur == kleur);
            }
            if (merk == null && min_prijs > 0 && max_prijs != null && kleur != null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Cases kleur
            //Als kleur niet leeg is
            if (merk == null && min_prijs == 0 && max_prijs == null && kleur != null && type == null)
            {
                horloges = _context.Horloges.Where(p => p.Kleur == kleur);
            }
            if (merk == null && min_prijs == 0 && max_prijs == null && kleur != null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Kleur == kleur && p.Type == type);
            }
            if (merk == null && min_prijs == 0 && max_prijs == null && kleur == null && type != null)
            {
                horloges = _context.Horloges.Where(p => p.Type == type);
            }

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
        public async Task<IActionResult> Create([Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId")] Horloge horloge)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId")] Horloge horloge)
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
            WishlistController wishlistcontroller = new WishlistController(_context);

            await wishlistcontroller.DeleteAllFromWishlist(id, "Horloge");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorlogeExists(int id)
        {
            return _context.Horloges.Any(e => e.Id == id);
        }
    }
}
