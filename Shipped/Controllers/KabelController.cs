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
    public class KabelController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly ApplicationDbContext _context;

        public KabelController(ApplicationDbContext context, IEmailSender emailSender, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
            _emailSender = emailSender;

        }

        // GET: Kabel
        public async Task<IActionResult> Index(string searchString, string sortOrder, string merk, int min_prijs, int? max_prijs, string lengte, string type)
        {
            ViewBag.NaamSortParm = String.IsNullOrEmpty(sortOrder) ? "naam" : "";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.PrijsSortParm = sortOrder == "prijs" ? "prijs_desc" : "prijs";
            ViewBag.MerkSortParm = sortOrder == "merk" ? "merk_desc" : "merk";
            ViewBag.LengteSortParm = sortOrder == "lengte" ? "lengte_desc" : "lengte";
            ViewBag.AantalSortParm = sortOrder == "aantal" ? "aantal_desc" : "aantal";
            ViewBag.Aantal_gekochtSortParm = sortOrder == "aantal_gekocht" ? "aantal_gekocht_desc" : "aantal_gekocht";
            ViewBag.Alles = _context.Kabels.GroupBy(p => new { p.Type, p.Merk, p.Lengte })
                            .Select(g => g.First())
                            .ToList();
            var kabels = from a in _context.Kabels.Include(d => d.Categorie) select a;
            if (merk == null && min_prijs == 0 && max_prijs == null && lengte == null && type == null)
            {
                kabels = from a in _context.Kabels.Include(d => d.Categorie) select a;
            }
            //Als alles niet leeg is
            if (merk != null && min_prijs > 0 && max_prijs != null && lengte != null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Lengte.ToString() == lengte && p.Type == type);
            }

            //Cases merk
            //Als merk en lengte niet leeg zijn
            if (merk != null && min_prijs == 0 && max_prijs == null && lengte != null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Lengte.ToString() == lengte);
            }
            if (merk != null && min_prijs == 0 && max_prijs == null && lengte != null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Lengte.ToString() == lengte && p.Type == type);
            }
            // Als merk niet leeg is
            if (merk != null && min_prijs == 0 && max_prijs == null && lengte == null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk);
            }

            if (merk != null && min_prijs == 0 && max_prijs == null && lengte == null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Type == type);
            }

            //Als merk en min_prijs niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs == null && lengte == null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Prijs >= min_prijs);
            }

            if (merk != null && min_prijs > 0 && max_prijs == null && lengte == null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Type == type);
            }

            //Als merk en min_prijs en lengte niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs == null && lengte != null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Lengte.ToString() == lengte);
            }

            if (merk != null && min_prijs > 0 && max_prijs == null && lengte != null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Lengte.ToString() == lengte && p.Type == type);
            }

            //Als merk en max_prijs niet leeg zijn
            if (merk != null && min_prijs == 0 && max_prijs != null && lengte == null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Prijs <= max_prijs);
            }

            if (merk != null && min_prijs == 0 && max_prijs != null && lengte == null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Prijs <= max_prijs && p.Type == type);
            }

            //Als merk en max_prijs en lengte niet leeg zijn
            if (merk != null && min_prijs == 0 && max_prijs != null && lengte != null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Prijs <= max_prijs && p.Lengte.ToString() == lengte);
            }
            if (merk != null && min_prijs == 0 && max_prijs != null && lengte != null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Prijs <= max_prijs && p.Lengte.ToString() == lengte && p.Type == type);
            }

            //Als merk en min_prijs en max_prijs niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs != null && lengte == null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs);
            }
            if (merk != null && min_prijs > 0 && max_prijs != null && lengte == null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Type == type);
            }

            //Cases min_prijs & max_prijs
            //Als min_prijs niet leeg is
            if (merk == null && min_prijs > 0 && max_prijs == null && lengte == null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Prijs >= min_prijs);
            }
            if (merk == null && min_prijs > 0 && max_prijs == null && lengte == null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Prijs >= min_prijs && p.Type == type);
            }

            //Als min_prijs en lengte niet leeg zijn
            if (merk == null && min_prijs > 0 && max_prijs == null && lengte != null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Prijs >= min_prijs && p.Lengte.ToString() == lengte);
            }
            if (merk == null && min_prijs > 0 && max_prijs == null && lengte != null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Prijs >= min_prijs && p.Lengte.ToString() == lengte && p.Type == type);
            }

            //Als max_prijs niet leeg is
            if (merk == null && min_prijs == 0 && max_prijs != null && lengte == null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Prijs <= max_prijs);
            }
            if (merk == null && min_prijs == 0 && max_prijs != null && lengte == null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Prijs <= max_prijs && p.Type == type);
            }

            //Als max_prijs en lengte niet leeg zijn
            if (merk == null && min_prijs == 0 && max_prijs != null && lengte != null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Prijs <= max_prijs && p.Lengte.ToString() == lengte);
            }
            if (merk == null && min_prijs == 0 && max_prijs != null && lengte != null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Prijs <= max_prijs && p.Lengte.ToString() == lengte && p.Type == type);
            }

            //Als min_prijs en max_prijs niet leeg zijn
            if (merk == null && min_prijs > 0 && max_prijs != null && lengte == null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs);
            }
            if (merk == null && min_prijs > 0 && max_prijs != null && lengte == null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Type == type);
            }
            //Als min_prijs en max_prijs en lengte niet leeg zijn
            if (merk == null && min_prijs > 0 && max_prijs != null && lengte != null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Lengte.ToString() == lengte);
            }
            if (merk == null && min_prijs > 0 && max_prijs != null && lengte != null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Lengte.ToString() == lengte && p.Type == type);
            }

            //Cases lengte
            //Als lengte niet leeg is
            if (merk == null && min_prijs == 0 && max_prijs == null && lengte != null && type == null)
            {
                kabels = _context.Kabels.Where(p => p.Lengte.ToString() == lengte);
            }
            if (merk == null && min_prijs == 0 && max_prijs == null && lengte != null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Lengte.ToString() == lengte && p.Type == type);
            }
            if (merk == null && min_prijs == 0 && max_prijs == null && lengte == null && type != null)
            {
                kabels = _context.Kabels.Where(p => p.Type == type);
            }
            switch (sortOrder)
            {
                case "naam":
                    kabels = kabels.OrderByDescending(s => s.Naam);
                    break;
                case "type":
                    kabels = kabels.OrderBy(s => s.Type);
                    break;
                case "type_desc":
                    kabels = kabels.OrderByDescending(s => s.Type);
                    break;
                case "prijs":
                    kabels = kabels.OrderBy(s => s.Prijs);
                    break;
                case "prijs_desc":
                    kabels = kabels.OrderByDescending(s => s.Prijs);
                    break;
                case "merk":
                    kabels = kabels.OrderBy(s => s.Merk);
                    break;
                case "merk_desc":
                    kabels = kabels.OrderByDescending(s => s.Merk);
                    break;
                case "lengte":
                    kabels = kabels.OrderBy(s => s.Lengte);
                    break;
                case "lengte_desc":
                    kabels = kabels.OrderByDescending(s => s.Lengte);
                    break;
                case "aantal":
                    kabels = kabels.OrderBy(s => s.Aantal);
                    break;
                case "aantal_desc":
                    kabels = kabels.OrderByDescending(s => s.Aantal);
                    break;
                case "aantal_gekocht":
                    kabels = kabels.OrderBy(s => s.Aantal_gekocht);
                    break;
                case "aantal_gekocht_desc":
                    kabels = kabels.OrderByDescending(s => s.Aantal_gekocht);
                    break;
                default:
                    kabels = kabels.OrderBy(s => s.Naam);
                    break;
            }

            return View(await kabels.ToListAsync());
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

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Type,Naam,Prijs,Merk,Lengte,Aantal,Afbeelding,Aantal_gekocht,CategorieId")] Kabel kabel)
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Naam,Prijs,Merk,Lengte,Aantal,Afbeelding,Aantal_gekocht,CategorieId")] Kabel kabel)
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kabel = await _context.Kabels.SingleOrDefaultAsync(m => m.Id == id);
            _context.Kabels.Remove(kabel);
            HomeController controller = new HomeController(_context, _emailSender, _manager);

            await controller.DeleteAllFromShoppingCart(id, "Kabel");
            WishlistController wishlistcontroller = new WishlistController(_context);
            await wishlistcontroller.DeleteAllFromWishlist(id, "Kabel");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KabelExists(int id)
        {
            return _context.Kabels.Any(e => e.Id == id);
        }
    }
}
