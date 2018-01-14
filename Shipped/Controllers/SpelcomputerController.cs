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
    public class SpelcomputerController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly ApplicationDbContext _context;

        public SpelcomputerController(ApplicationDbContext context, IEmailSender emailSender, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
            _emailSender = emailSender;

        }


        // GET: Spelcomputer
        public async Task<IActionResult> Index(string searchString, string sortOrder, string merk, int min_prijs, int? max_prijs, string geheugen, string type)
        {
            ViewBag.NaamSortParm = String.IsNullOrEmpty(sortOrder) ? "naam" : "";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.PrijsSortParm = sortOrder == "prijs" ? "prijs_desc" : "prijs";
            ViewBag.MerkSortParm = sortOrder == "merk" ? "merk_desc" : "merk";
            ViewBag.GeheugenSortParm = sortOrder == "geheugen" ? "geheugen_desc" : "geheugen";
            ViewBag.AantalSortParm = sortOrder == "aantal" ? "aantal_desc" : "aantal";
            ViewBag.Aantal_gekochtSortParm = sortOrder == "aantal_gekocht" ? "aantal_gekocht_desc" : "aantal_gekocht";
            ViewBag.AllesTypes = _context.Spelcomputers.GroupBy(p => new { p.Type})
                            .Select(g => g.First())
                            .ToList();
            ViewBag.AllesMerken = _context.Spelcomputers.GroupBy(p => new {p.Merk})
                            .Select(g => g.First())
                            .ToList();
             ViewBag.AllesGeheugens = _context.Spelcomputers.GroupBy(p => new {p.Geheugen })
                            .Select(g => g.First())
                            .ToList();
            var spelcomputers = from a in _context.Spelcomputers.Include(d => d.Categorie) select a;
            //Als alles leeg is
            if (merk == null && min_prijs == 0 && max_prijs == null && geheugen == null && type == null)
            {
                spelcomputers = from a in _context.Spelcomputers.Include(d => d.Categorie) select a;
            }
            //Als alles niet leeg is
            if (merk != null && min_prijs > 0 && max_prijs != null && geheugen != null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Geheugen.ToString() == geheugen && p.Type == type);
            }

            //Cases merk
            //Als merk en geheugen niet leeg zijn
            if (merk != null && min_prijs == 0 && max_prijs == null && geheugen != null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Geheugen.ToString() == geheugen);
            }
            if (merk != null && min_prijs == 0 && max_prijs == null && geheugen != null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Geheugen.ToString() == geheugen && p.Type == type);
            }
            // Als merk niet leeg is
            if (merk != null && min_prijs == 0 && max_prijs == null && geheugen == null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk);
            }

            if (merk != null && min_prijs == 0 && max_prijs == null && geheugen == null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Type == type);
            }

            //Als merk en min_prijs niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs == null && geheugen == null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Prijs >= min_prijs);
            }

            if (merk != null && min_prijs > 0 && max_prijs == null && geheugen == null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Type == type);
            }

            //Als merk en min_prijs en geheugen niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs == null && geheugen != null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Geheugen.ToString() == geheugen);
            }

            if (merk != null && min_prijs > 0 && max_prijs == null && geheugen != null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Geheugen.ToString() == geheugen && p.Type == type);
            }

            //Als merk en max_prijs niet leeg zijn
            if (merk != null && min_prijs == 0 && max_prijs != null && geheugen == null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Prijs <= max_prijs);
            }

            if (merk != null && min_prijs == 0 && max_prijs != null && geheugen == null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Prijs <= max_prijs && p.Type == type);
            }

            //Als merk en max_prijs en geheugen niet leeg zijn
            if (merk != null && min_prijs == 0 && max_prijs != null && geheugen != null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Prijs <= max_prijs && p.Geheugen.ToString() == geheugen);
            }
            if (merk != null && min_prijs == 0 && max_prijs != null && geheugen != null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Prijs <= max_prijs && p.Geheugen.ToString() == geheugen && p.Type == type);
            }

            //Als merk en min_prijs en max_prijs niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs != null && geheugen == null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs);
            }
            if (merk != null && min_prijs > 0 && max_prijs != null && geheugen == null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Type == type);
            }

            //Cases min_prijs & max_prijs
            //Als min_prijs niet leeg is
            if (merk == null && min_prijs > 0 && max_prijs == null && geheugen == null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Prijs >= min_prijs);
            }
            if (merk == null && min_prijs > 0 && max_prijs == null && geheugen == null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Prijs >= min_prijs && p.Type == type);
            }

            //Als min_prijs en geheugen niet leeg zijn
            if (merk == null && min_prijs > 0 && max_prijs == null && geheugen != null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Prijs >= min_prijs && p.Geheugen.ToString() == geheugen);
            }
            if (merk == null && min_prijs > 0 && max_prijs == null && geheugen != null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Prijs >= min_prijs && p.Geheugen.ToString() == geheugen && p.Type == type);
            }

            //Als max_prijs niet leeg is
            if (merk == null && min_prijs == 0 && max_prijs != null && geheugen == null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Prijs <= max_prijs);
            }
            if (merk == null && min_prijs == 0 && max_prijs != null && geheugen == null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Prijs <= max_prijs && p.Type == type);
            }

            //Als max_prijs en geheugen niet leeg zijn
            if (merk == null && min_prijs == 0 && max_prijs != null && geheugen != null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Prijs <= max_prijs && p.Geheugen.ToString() == geheugen);
            }
            if (merk == null && min_prijs == 0 && max_prijs != null && geheugen != null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Prijs <= max_prijs && p.Geheugen.ToString() == geheugen && p.Type == type);
            }

            //Als min_prijs en max_prijs niet leeg zijn
            if (merk == null && min_prijs > 0 && max_prijs != null && geheugen == null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs);
            }
            if (merk == null && min_prijs > 0 && max_prijs != null && geheugen == null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Type == type);
            }
            //Als min_prijs en max_prijs en geheugen niet leeg zijn
            if (merk == null && min_prijs > 0 && max_prijs != null && geheugen != null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Geheugen.ToString() == geheugen);
            }
            if (merk == null && min_prijs > 0 && max_prijs != null && geheugen != null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Geheugen.ToString() == geheugen && p.Type == type);
            }

            //Cases geheugen
            //Als geheugen niet leeg is
            if (merk == null && min_prijs == 0 && max_prijs == null && geheugen != null && type == null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Geheugen.ToString() == geheugen);
            }
            if (merk == null && min_prijs == 0 && max_prijs == null && geheugen != null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Geheugen.ToString() == geheugen && p.Type == type);
            }
            if (merk == null && min_prijs == 0 && max_prijs == null && geheugen == null && type != null)
            {
                spelcomputers = _context.Spelcomputers.Where(p => p.Type == type);
            }
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
                case "geheugen":
                    spelcomputers = spelcomputers.OrderBy(s => s.Geheugen);
                    break;
                case "geheugen_desc":
                    spelcomputers = spelcomputers.OrderByDescending(s => s.Geheugen);
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Type,Naam,Prijs,Merk,Geheugen,Aantal,Afbeelding,Aantal_gekocht,CategorieId")] Spelcomputer spelcomputer)
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Naam,Prijs,Merk,Geheugen,Aantal,Afbeelding,Aantal_gekocht,CategorieId")] Spelcomputer spelcomputer)
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spelcomputer = await _context.Spelcomputers.FirstOrDefaultAsync(m => m.Id == id);
            _context.Spelcomputers.Remove(spelcomputer);
            HomeController controller = new HomeController(_context, _emailSender, _manager);

            await controller.DeleteAllFromShoppingCart(id, "Spelcomputer");
            WishlistController wishlistcontroller = new WishlistController(_context);
            await wishlistcontroller.DeleteAllFromWishlist(id, "Spelcomputer");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpelcomputerExists(int id)
        {
            return _context.Spelcomputers.Any(e => e.Id == id);
        }
    }
}
