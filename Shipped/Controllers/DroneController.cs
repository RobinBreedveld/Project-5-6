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
            ViewBag.NaamSortParm = String.IsNullOrEmpty(sortOrder) ? "naam" : "";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.PrijsSortParm = sortOrder == "prijs" ? "prijs_desc" : "prijs";
            ViewBag.MerkSortParm = sortOrder == "merk" ? "merk_desc" : "merk";
            ViewBag.KleurSortParm = sortOrder == "kleur" ? "kleur_desc" : "kleur";
            ViewBag.AantalSortParm = sortOrder == "aantal" ? "aantal_desc" : "aantal";
            ViewBag.Aantal_gekochtSortParm = sortOrder == "aantal_gekocht" ? "aantal_gekocht_desc" : "aantal_gekocht";
            ViewBag.Aantal_rotorsSortParm = sortOrder == "aantal_rotors" ? "aantal_rotors_desc" : "aantal_rotors";
            ViewBag.GrootteSortParm = sortOrder == "grootte" ? "grootte_desc" : "grootte";
            
            var drones = from a in _context.Drones.Include(d => d.Categorie) select a;

            switch (sortOrder)
            {
                case "naam":
                    drones = drones.OrderByDescending(s => s.Naam);
                    break;
                case "type":
                    drones = drones.OrderBy(s => s.Type);
                    break;
                case "type_desc":
                    drones = drones.OrderByDescending(s => s.Type);
                    break;
                case "prijs":
                    drones = drones.OrderBy(s => s.Prijs);
                    break;
                case "prijs_desc":
                    drones = drones.OrderByDescending(s => s.Prijs);
                    break;   
                case "merk":
                    drones = drones.OrderBy(s => s.Merk);
                    break;
                case "merk_desc":
                    drones = drones.OrderByDescending(s => s.Merk);
                    break;
                case "kleur":
                    drones = drones.OrderBy(s => s.Kleur);
                    break;
                case "kleur_desc":
                    drones = drones.OrderByDescending(s => s.Kleur);
                    break;
                case "aantal":
                    drones = drones.OrderBy(s => s.Aantal);
                    break; 
                case "aantal_desc":
                    drones = drones.OrderByDescending(s => s.Aantal);
                    break;
                case "aantal_gekocht":
                    drones = drones.OrderBy(s => s.Aantal_gekocht);
                    break; 
                case "aantal_gekocht_desc":
                    drones = drones.OrderByDescending(s => s.Aantal_gekocht);
                    break;
                case "aantal_rotors":
                    drones = drones.OrderBy(s => s.Aantal_rotors);
                    break; 
                case "aantal_rotors_desc":
                    drones = drones.OrderByDescending(s => s.Aantal_rotors);
                    break; 
                case "grootte":
                    drones = drones.OrderBy(s => s.Grootte);
                    break; 
                case "grootte_desc":
                    drones = drones.OrderByDescending(s => s.Grootte);
                    break;         
                default:
                     drones = drones.OrderBy(s => s.Naam);
                    break;
            }

        return View(await drones.ToListAsync());
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
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles="Admin")]
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
