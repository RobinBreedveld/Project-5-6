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

    public async Task<IActionResult> Index(string searchString, string sortOrder, string merk, int min_prijs, int? max_prijs, string kleur, string type)
        {
            ViewBag.NaamSortParm = String.IsNullOrEmpty(sortOrder) ? "naam" : "";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.PrijsSortParm = sortOrder == "prijs" ? "prijs_desc" : "prijs";
            ViewBag.MerkSortParm = sortOrder == "merk" ? "merk_desc" : "merk";
            ViewBag.KleurSortParm = sortOrder == "kleur" ? "kleur_desc" : "kleur";
            ViewBag.AantalSortParm = sortOrder == "aantal" ? "aantal_desc" : "aantal";
            ViewBag.Aantal_gekochtSortParm = sortOrder == "aantal_gekocht" ? "aantal_gekocht_desc" : "aantal_gekocht";
            ViewBag.Alles = _context.Drones.GroupBy(p => new { p.Type, p.Merk, p.Kleur} )
                            .Select(g => g.First())
                            .ToList();
            var drones = from a in _context.Drones.Include(d => d.Categorie) select a;
            //Als alles leeg is
            if( merk == null && min_prijs == 0 && max_prijs == null && kleur == null && type == null) {
                drones = from a in _context.Drones.Include(d => d.Categorie) select a;
            }
            //Als alles niet leeg is
            if (merk != null && min_prijs > 0 && max_prijs != null && kleur != null && type == null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Kleur ==  kleur && p.Type == type);
            }

            //Cases merk
            //Als merk en kleur niet leeg zijn
            if( merk != null && min_prijs == 0 && max_prijs == null && kleur != null && type == null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Kleur == kleur);
            }
            if( merk != null && min_prijs == 0 && max_prijs == null && kleur != null && type != null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Kleur == kleur && p.Type == type);
            }
            // Als merk niet leeg is
            if ( merk != null && min_prijs == 0 && max_prijs == null && kleur == null && type == null) {
                drones = _context.Drones.Where( p => p.Merk == merk);
            }

            if ( merk != null && min_prijs == 0 && max_prijs == null && kleur == null && type != null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Type == type);
            }
            
            //Als merk en min_prijs niet leeg zijn
            if ( merk != null && min_prijs > 0 && max_prijs == null && kleur == null && type == null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Prijs >= min_prijs);
            }

            if ( merk != null && min_prijs > 0 && max_prijs == null && kleur == null && type != null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Prijs >= min_prijs && p.Type == type);
            }

            //Als merk en min_prijs en kleur niet leeg zijn
            if ( merk != null && min_prijs > 0 && max_prijs == null && kleur != null && type == null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Prijs >= min_prijs && p.Kleur == kleur);
            }

            if ( merk != null && min_prijs > 0 && max_prijs == null && kleur != null && type != null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Prijs >= min_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Als merk en max_prijs niet leeg zijn
            if ( merk != null && min_prijs == 0 && max_prijs != null && kleur == null && type == null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Prijs <= max_prijs);
            }

            if ( merk != null && min_prijs == 0 && max_prijs != null && kleur == null && type != null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Prijs <= max_prijs && p.Type == type);
            }

            //Als merk en max_prijs en kleur niet leeg zijn
            if ( merk != null && min_prijs == 0 && max_prijs != null && kleur != null && type == null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Prijs <= max_prijs && p.Kleur == kleur);
            }
            if ( merk != null && min_prijs == 0 && max_prijs != null && kleur != null && type != null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Prijs <= max_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Als merk en min_prijs en max_prijs niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs != null && kleur == null && type == null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs);
            }
            if (merk != null && min_prijs > 0 && max_prijs != null && kleur == null && type != null) {
                drones = _context.Drones.Where( p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Type == type);
            }

            //Cases min_prijs & max_prijs
            //Als min_prijs niet leeg is
            if( merk == null && min_prijs > 0 && max_prijs == null && kleur == null && type == null){
                 drones = _context.Drones.Where( p => p.Prijs >= min_prijs );
            }
            if( merk == null && min_prijs > 0 && max_prijs == null && kleur == null && type != null){
                 drones = _context.Drones.Where( p => p.Prijs >= min_prijs && p.Type == type);
            }

            //Als min_prijs en kleur niet leeg zijn
            if( merk == null && min_prijs > 0 && max_prijs == null && kleur != null && type == null){
                 drones = _context.Drones.Where( p => p.Prijs >= min_prijs && p.Kleur == kleur);
            }
            if( merk == null && min_prijs > 0 && max_prijs == null && kleur != null && type != null){
                 drones = _context.Drones.Where( p => p.Prijs >= min_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Als max_prijs niet leeg is
            if( merk == null && min_prijs == 0 && max_prijs != null && kleur == null && type == null){
                 drones = _context.Drones.Where( p => p.Prijs <= max_prijs );
            }
            if( merk == null && min_prijs == 0 && max_prijs != null && kleur == null && type != null){
                 drones = _context.Drones.Where( p => p.Prijs <= max_prijs && p.Type == type);
            }

            //Als max_prijs en kleur niet leeg zijn
            if( merk == null && min_prijs == 0 && max_prijs != null && kleur != null && type == null){
                 drones = _context.Drones.Where( p => p.Prijs <= max_prijs && p.Kleur == kleur);
            }
            if( merk == null && min_prijs == 0 && max_prijs != null && kleur != null && type != null){
                 drones = _context.Drones.Where( p => p.Prijs <= max_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Als min_prijs en max_prijs niet leeg zijn
            if( merk == null && min_prijs > 0 && max_prijs != null && kleur == null && type == null){
                 drones = _context.Drones.Where( p => p.Prijs >= min_prijs && p.Prijs <= max_prijs );
            }
            if( merk == null && min_prijs > 0 && max_prijs != null && kleur == null && type != null){
                 drones = _context.Drones.Where( p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Type == type);
            }
            //Als min_prijs en max_prijs en kleur niet leeg zijn
            if( merk == null && min_prijs > 0 && max_prijs != null && kleur != null && type == null){
                 drones = _context.Drones.Where( p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Kleur == kleur);
            }
            if( merk == null && min_prijs > 0 && max_prijs != null && kleur != null && type != null){
                 drones = _context.Drones.Where( p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Kleur == kleur && p.Type == type);
            }

            //Cases kleur
            //Als kleur niet leeg is
            if( merk == null && min_prijs == 0 && max_prijs == null && kleur != null && type == null){
                 drones = _context.Drones.Where( p =>  p.Kleur == kleur);
            }
            if( merk == null && min_prijs == 0 && max_prijs == null && kleur != null && type != null){
                 drones = _context.Drones.Where( p =>  p.Kleur == kleur && p.Type == type);
            }
            if( merk == null && min_prijs == 0 && max_prijs == null && kleur == null && type != null){
                 drones = _context.Drones.Where( p => p.Type == type);
            }

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
        public async Task<IActionResult> Create([Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId")] Drone drone)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Naam,Prijs,Merk,Kleur,Aantal,Afbeelding,Aantal_gekocht,CategorieId")] Drone drone)
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
            var drone = await _context.Drones.FirstOrDefaultAsync(m => m.Id == id);
            _context.Drones.Remove(drone);
            HomeController controller = new HomeController(_context);
            await controller.DeleteAllFromShoppingCart(id, "Drone");
            WishlistController wishlistcontroller = new WishlistController(_context);
            await wishlistcontroller.DeleteAllFromWishlist(id, "Drone");
            await _context.SaveChangesAsync();            
            return RedirectToAction(nameof(Index));
        }

        private bool DroneExists(int id)
        {
            return _context.Drones.Any(e => e.Id == id);
        }
    }
}
