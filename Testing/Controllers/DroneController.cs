using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using login2.Data;
using login2.Models;
using Microsoft.AspNetCore.Identity;
using System.Web;
using System.Security.Principal;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using login2.Models.ManageViewModels;
using login2.Services;
namespace login2.Controllers
{
    public class DroneController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DroneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Drone
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var drones = from a in _context.Drones select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                drones = _context.Drones.Where(s => s.Naam.StartsWith(searchString.ToUpper()));
            }
            else
            {
                drones = from a in _context.Drones select a;
            }

            var applicationDbContext = _context.Drones.Include(d => d.Categorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Drone/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drone = await _context.Drones
                .Include(d => d.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (drone == null)
            {
                return NotFound();
            }

            return View(drone);
        }
        

        // GET: Drone/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Drone/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,Aantal_rotors,Grootte")] Drone drone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", drone.CategorieId);
            return View(drone);
        }

        // GET: Drone/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drone = await _context.Drones.SingleOrDefaultAsync(m => m.Id == id);
            if (drone == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", drone.CategorieId);
            return View(drone);
        }

        // POST: Drone/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId,Aantal_rotors,Grootte")] Drone drone)
        {
            if (id != drone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DroneExists(drone.Id))
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
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", drone.CategorieId);
            return View(drone);
        }

        // GET: Drone/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drone = await _context.Drones
                .Include(d => d.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (drone == null)
            {
                return NotFound();
            }

            return View(drone);
        }

        // POST: Drone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drone = await _context.Drones.SingleOrDefaultAsync(m => m.Id == id);
            _context.Drones.Remove(drone);
            await _context.SaveChangesAsync();
            //deletes cartitem with same id as deleted item
            var delete = await _context.Cart.SingleOrDefaultAsync(m => m.Product_Id == id && m.Model_naam == "Drone");
            if (delete != null){
            _context.Cart.Remove(delete);
            await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DroneExists(int id)
        {
            return _context.Drones.Any(e => e.Id == id);
        }
    }
}
