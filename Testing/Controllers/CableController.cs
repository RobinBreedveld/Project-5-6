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
    public class CableController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CableController(ApplicationDbContext context)
        {
            _context = context;
        }

       // GET: Cable
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.LengthSortParm = sortOrder == "length" ? "length_desc" : "length";
            var cables = from a in _context.Cables select a;
             if (!String.IsNullOrEmpty(searchString))
            {
                cables = _context.Cables.Where(s => s.Name.StartsWith(searchString.ToUpper()));
            }
            else
            {
                cables = from a in _context.Cables select a;
            }
            switch (sortOrder)
            {
                case "name":
                    cables = cables.OrderByDescending(s => s.Name);
                    break;
                case "price":
                    cables = cables.OrderBy(s => s.Price);
                    break;
                 case "price_desc":
                    cables = cables.OrderByDescending(s => s.Price);
                    break;    
                case "length":
                    cables = cables.OrderBy(s => s.Length);;
                    break;
                case "length_desc":
                    cables = cables.OrderByDescending(s => s.Length);;
                    break;    
                default:
                     cables = cables.OrderBy(s => s.Name);
                    break;
            }
            return View(await cables.ToListAsync());
        }


        // GET: Cable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cable = await _context.Cables
                .Include(c => c.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (cable == null)
            {
                return NotFound();
            }

            return View(cable);
        }

        // GET: Cable/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Cable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Length,Type,CategorieId")] Cable cable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", cable.CategorieId);
            return View(cable);
        }

        // GET: Cable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cable = await _context.Cables.SingleOrDefaultAsync(m => m.Id == id);
            if (cable == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", cable.CategorieId);
            return View(cable);
        }

        // POST: Cable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Length,Type,CategorieId")] Cable cable)
        {
            if (id != cable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CableExists(cable.Id))
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
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", cable.CategorieId);
            return View(cable);
        }

        // GET: Cable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cable = await _context.Cables
                .Include(c => c.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (cable == null)
            {
                return NotFound();
            }

            return View(cable);
        }

        // POST: Cable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cable = await _context.Cables.SingleOrDefaultAsync(m => m.Id == id);
            _context.Cables.Remove(cable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CableExists(int id)
        {
            return _context.Cables.Any(e => e.Id == id);
        }
    }
}
