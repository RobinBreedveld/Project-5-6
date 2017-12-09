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
    public class SpecsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Specs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Specs.Include(s => s.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Specs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spec = await _context.Specs
                .Include(s => s.Product)
                .SingleOrDefaultAsync(m => m.SpecId == id);
            if (spec == null)
            {
                return NotFound();
            }

            return View(spec);
        }

        // GET: Specs/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: Specs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpecId,Name,Intvalue,Stringvalue,ProductId")] Spec spec)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spec);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", spec.ProductId);
            return View(spec);
        }

        // GET: Specs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spec = await _context.Specs.SingleOrDefaultAsync(m => m.SpecId == id);
            if (spec == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", spec.ProductId);
            return View(spec);
        }

        // POST: Specs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpecId,Name,Intvalue,Stringvalue,ProductId")] Spec spec)
        {
            if (id != spec.SpecId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spec);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecExists(spec.SpecId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", spec.ProductId);
            return View(spec);
        }

        // GET: Specs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spec = await _context.Specs
                .Include(s => s.Product)
                .SingleOrDefaultAsync(m => m.SpecId == id);
            if (spec == null)
            {
                return NotFound();
            }

            return View(spec);
        }

        // POST: Specs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spec = await _context.Specs.SingleOrDefaultAsync(m => m.SpecId == id);
            _context.Specs.Remove(spec);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecExists(int id)
        {
            return _context.Specs.Any(e => e.SpecId == id);
        }
    }
}
