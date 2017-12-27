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
    public class FotocameraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FotocameraController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fotocamera
        public async Task<IActionResult> Index(string searchString, string sortOrder, string merk, int min_prijs, int? max_prijs, string megapixels, string type)
        {
            ViewBag.NaamSortParm = String.IsNullOrEmpty(sortOrder) ? "naam" : "";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.PrijsSortParm = sortOrder == "prijs" ? "prijs_desc" : "prijs";
            ViewBag.MerkSortParm = sortOrder == "merk" ? "merk_desc" : "merk";
            ViewBag.MegapixelsSortParm = sortOrder == "megapixels" ? "megapixels_desc" : "megapixels";
            ViewBag.AantalSortParm = sortOrder == "aantal" ? "aantal_desc" : "aantal";
            ViewBag.Aantal_gekochtSortParm = sortOrder == "aantal_gekocht" ? "aantal_gekocht_desc" : "aantal_gekocht";
            ViewBag.Alles = _context.Fotocameras.GroupBy(p => new { p.Type, p.Merk, p.Megapixels} )
                            .Select(g => g.First())
                            .ToList();
            var fotocameras = from a in _context.Fotocameras.Include(d => d.Categorie) select a;
             //Als alles leeg is
            if( merk == null && min_prijs == 0 && max_prijs == null && megapixels == null && type == null) {
                fotocameras = from a in _context.Fotocameras.Include(d => d.Categorie) select a;
            }
            //Als alles niet leeg is
            if (merk != null && min_prijs > 0 && max_prijs != null && megapixels != null && type == null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Megapixels ==  megapixels && p.Type == type);
            }

            //Cases merk
            //Als merk en megapixels niet leeg zijn
            if( merk != null && min_prijs == 0 && max_prijs == null && megapixels != null && type == null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Megapixels == megapixels);
            }
            if( merk != null && min_prijs == 0 && max_prijs == null && megapixels != null && type != null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Megapixels == megapixels && p.Type == type);
            }
            // Als merk niet leeg is
            if ( merk != null && min_prijs == 0 && max_prijs == null && megapixels == null && type == null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk);
            }

            if ( merk != null && min_prijs == 0 && max_prijs == null && megapixels == null && type != null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Type == type);
            }
            
            //Als merk en min_prijs niet leeg zijn
            if ( merk != null && min_prijs > 0 && max_prijs == null && megapixels == null && type == null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Prijs >= min_prijs);
            }

            if ( merk != null && min_prijs > 0 && max_prijs == null && megapixels == null && type != null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Prijs >= min_prijs && p.Type == type);
            }

            //Als merk en min_prijs en megapixels niet leeg zijn
            if ( merk != null && min_prijs > 0 && max_prijs == null && megapixels != null && type == null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Prijs >= min_prijs && p.Megapixels == megapixels);
            }

            if ( merk != null && min_prijs > 0 && max_prijs == null && megapixels != null && type != null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Prijs >= min_prijs && p.Megapixels == megapixels && p.Type == type);
            }

            //Als merk en max_prijs niet leeg zijn
            if ( merk != null && min_prijs == 0 && max_prijs != null && megapixels == null && type == null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Prijs <= max_prijs);
            }

            if ( merk != null && min_prijs == 0 && max_prijs != null && megapixels == null && type != null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Prijs <= max_prijs && p.Type == type);
            }

            //Als merk en max_prijs en megapixels niet leeg zijn
            if ( merk != null && min_prijs == 0 && max_prijs != null && megapixels != null && type == null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Prijs <= max_prijs && p.Megapixels == megapixels);
            }
            if ( merk != null && min_prijs == 0 && max_prijs != null && megapixels != null && type != null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Prijs <= max_prijs && p.Megapixels == megapixels && p.Type == type);
            }

            //Als merk en min_prijs en max_prijs niet leeg zijn
            if (merk != null && min_prijs > 0 && max_prijs != null && megapixels == null && type == null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs);
            }
            if (merk != null && min_prijs > 0 && max_prijs != null && megapixels == null && type != null) {
                fotocameras = _context.Fotocameras.Where( p => p.Merk == merk && p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Type == type);
            }

            //Cases min_prijs & max_prijs
            //Als min_prijs niet leeg is
            if( merk == null && min_prijs > 0 && max_prijs == null && megapixels == null && type == null){
                 fotocameras = _context.Fotocameras.Where( p => p.Prijs >= min_prijs );
            }
            if( merk == null && min_prijs > 0 && max_prijs == null && megapixels == null && type != null){
                 fotocameras = _context.Fotocameras.Where( p => p.Prijs >= min_prijs && p.Type == type);
            }

            //Als min_prijs en megapixels niet leeg zijn
            if( merk == null && min_prijs > 0 && max_prijs == null && megapixels != null && type == null){
                 fotocameras = _context.Fotocameras.Where( p => p.Prijs >= min_prijs && p.Megapixels == megapixels);
            }
            if( merk == null && min_prijs > 0 && max_prijs == null && megapixels != null && type != null){
                 fotocameras = _context.Fotocameras.Where( p => p.Prijs >= min_prijs && p.Megapixels == megapixels && p.Type == type);
            }

            //Als max_prijs niet leeg is
            if( merk == null && min_prijs == 0 && max_prijs != null && megapixels == null && type == null){
                 fotocameras = _context.Fotocameras.Where( p => p.Prijs <= max_prijs );
            }
            if( merk == null && min_prijs == 0 && max_prijs != null && megapixels == null && type != null){
                 fotocameras = _context.Fotocameras.Where( p => p.Prijs <= max_prijs && p.Type == type);
            }

            //Als max_prijs en megapixels niet leeg zijn
            if( merk == null && min_prijs == 0 && max_prijs != null && megapixels != null && type == null){
                 fotocameras = _context.Fotocameras.Where( p => p.Prijs <= max_prijs && p.Megapixels == megapixels);
            }
            if( merk == null && min_prijs == 0 && max_prijs != null && megapixels != null && type != null){
                 fotocameras = _context.Fotocameras.Where( p => p.Prijs <= max_prijs && p.Megapixels == megapixels && p.Type == type);
            }

            //Als min_prijs en max_prijs niet leeg zijn
            if( merk == null && min_prijs > 0 && max_prijs != null && megapixels == null && type == null){
                 fotocameras = _context.Fotocameras.Where( p => p.Prijs >= min_prijs && p.Prijs <= max_prijs );
            }
            if( merk == null && min_prijs > 0 && max_prijs != null && megapixels == null && type != null){
                 fotocameras = _context.Fotocameras.Where( p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Type == type);
            }
            //Als min_prijs en max_prijs en megapixels niet leeg zijn
            if( merk == null && min_prijs > 0 && max_prijs != null && megapixels != null && type == null){
                 fotocameras = _context.Fotocameras.Where( p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Megapixels == megapixels);
            }
            if( merk == null && min_prijs > 0 && max_prijs != null && megapixels != null && type != null){
                 fotocameras = _context.Fotocameras.Where( p => p.Prijs >= min_prijs && p.Prijs <= max_prijs && p.Megapixels == megapixels && p.Type == type);
            }

            //Cases megapixels
            //Als megapixels niet leeg is
            if( merk == null && min_prijs == 0 && max_prijs == null && megapixels != null && type == null){
                 fotocameras = _context.Fotocameras.Where( p =>  p.Megapixels == megapixels);
            }
            if( merk == null && min_prijs == 0 && max_prijs == null && megapixels != null && type != null){
                 fotocameras = _context.Fotocameras.Where( p =>  p.Megapixels == megapixels && p.Type == type);
            }
            if( merk == null && min_prijs == 0 && max_prijs == null && megapixels == null && type != null){
                 fotocameras = _context.Fotocameras.Where( p => p.Type == type);
            }

            //Cases megapixels
            //Als megapixels niet leeg is
            if( merk == null && min_prijs == 0 && max_prijs == 0 && megapixels != null){
                 fotocameras = _context.Fotocameras.Where( p =>  p.Megapixels == megapixels);
            }
            switch (sortOrder)
            {
                case "naam":
                    fotocameras = fotocameras.OrderByDescending(s => s.Naam);
                    break;
                case "type":
                    fotocameras = fotocameras.OrderBy(s => s.Type);
                    break;
                case "type_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Type);
                    break;
                case "prijs":
                    fotocameras = fotocameras.OrderBy(s => s.Prijs);
                    break;
                case "prijs_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Prijs);
                    break;   
                case "merk":
                    fotocameras = fotocameras.OrderBy(s => s.Merk);
                    break;
                case "merk_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Merk);
                    break;
                case "megapixels":
                    fotocameras = fotocameras.OrderBy(s => s.Megapixels);
                    break;
                case "megapixels_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Megapixels);
                    break;
                case "aantal":
                    fotocameras = fotocameras.OrderBy(s => s.Aantal);
                    break; 
                case "aantal_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Aantal);
                    break;
                case "aantal_gekocht":
                    fotocameras = fotocameras.OrderBy(s => s.Aantal_gekocht);
                    break; 
                case "aantal_gekocht_desc":
                    fotocameras = fotocameras.OrderByDescending(s => s.Aantal_gekocht);
                    break;        
                default:
                     fotocameras = fotocameras.OrderBy(s => s.Naam);
                    break;
            }

        return View(await fotocameras.ToListAsync());
        }

        // GET: Fotocamera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotocamera = await _context.Fotocameras
                .Include(f => f.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fotocamera == null)
            {
                return NotFound();
            }

            return View(fotocamera);
        }

        // GET: Fotocamera/Create
         [Authorize(Roles="Admin")]
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Fotocamera/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize(Roles="Admin")]
        public async Task<IActionResult> Create([Bind("Id,Type,Naam,Prijs,Merk,Megapixels,Aantal,Afbeelding,Aantal_gekocht,CategorieId")] Fotocamera fotocamera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fotocamera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", fotocamera.CategorieId);
            return View(fotocamera);
        }

        // GET: Fotocamera/Edit/5
         [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotocamera = await _context.Fotocameras.SingleOrDefaultAsync(m => m.Id == id);
            if (fotocamera == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", fotocamera.CategorieId);
            return View(fotocamera);
        }

        // POST: Fotocamera/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Naam,Prijs,Merk,Megapixels,Aantal,Afbeelding,Aantal_gekocht,CategorieId")] Fotocamera fotocamera)
        {
            if (id != fotocamera.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fotocamera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotocameraExists(fotocamera.Id))
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
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", fotocamera.CategorieId);
            return View(fotocamera);
        }

        // GET: Fotocamera/Delete/5
         [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var fotocamera = await _context.Fotocameras
                .Include(f => f.Categorie)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fotocamera == null)
            {
                return NotFound();
            }
            return View(fotocamera);
        }

        // POST: Fotocamera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotocamera = await _context.Fotocameras.FirstOrDefaultAsync(m => m.Id == id);
            _context.Fotocameras.Remove(fotocamera);            
            HomeController controller = new HomeController(_context);
            await controller.DeleteAllFromShoppingCart(id, "Fotocamera");
            WishlistController wishlistcontroller = new WishlistController(_context);
            await wishlistcontroller.DeleteAllFromWishlist(id, "Fotocamera");
            await _context.SaveChangesAsync();           
            return RedirectToAction(nameof(Index));
        }

        private bool FotocameraExists(int id)
        {
            return _context.Fotocameras.Any(e => e.Id == id);
        }
    }
}