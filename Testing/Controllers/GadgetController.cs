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
    public class GadgetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GadgetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gadget
        // GET: Gadget
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            var Gadget = from a in _context.Gadgets select a;
             if (!String.IsNullOrEmpty(searchString))
            {
                Gadget = _context.Gadgets.Where(s => s.Name.StartsWith(searchString.ToUpper()));
            }
            else
            {
                Gadget = from a in _context.Gadgets select a;
            }
            switch (sortOrder)
            {
                case "name":
                    Gadget = Gadget.OrderByDescending(s => s.Name);
                    break;
                case "price":
                    Gadget = Gadget.OrderBy(s => s.Price);
                    break;
                 case "price_desc":
                    Gadget = Gadget.OrderByDescending(s => s.Price);
                    break;    
                case "type":
                    Gadget = Gadget.OrderBy(s => s.Type);;
                    break;
                case "type_desc":
                    Gadget = Gadget.OrderByDescending(s => s.Type);;
                    break;    
                default:
                     Gadget = Gadget.OrderBy(s => s.Name);
                    break;
            }
            return View(await Gadget.ToListAsync());
        }


        // GET: Gadget/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gadget = await _context.Gadgets
                .Include(g => g.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (gadget == null)
            {
                return NotFound();
            }

            return View(gadget);
        }

        // GET: Gadget/Create
        [Authorize(Roles="Admin")]
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Gadget/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Type,CategorieId")] Gadget gadget)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gadget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", gadget.CategorieId);
            return View(gadget);
        }

        // GET: Gadget/Edit/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gadget = await _context.Gadgets.SingleOrDefaultAsync(m => m.Id == id);
            if (gadget == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", gadget.CategorieId);
            return View(gadget);
        }

        // POST: Gadget/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Type,CategorieId")] Gadget gadget)
        {
            if (id != gadget.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gadget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GadgetExists(gadget.Id))
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
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", gadget.CategorieId);
            return View(gadget);
        }

        // GET: Gadget/Delete/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gadget = await _context.Gadgets
                .Include(g => g.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (gadget == null)
            {
                return NotFound();
            }

            return View(gadget);
        }

        // POST: Gadget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gadget = await _context.Gadgets.SingleOrDefaultAsync(m => m.Id == id);
            _context.Gadgets.Remove(gadget);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GadgetExists(int id)
        {
            return _context.Gadgets.Any(e => e.Id == id);
        }
    }
}
