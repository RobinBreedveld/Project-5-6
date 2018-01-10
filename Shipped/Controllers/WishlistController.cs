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
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;
        public WishlistController(ApplicationDbContext context)
        {
            _context = context;
        }
        //public async Task<IActionResult> Index()

        [Authorize]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            //makes sure that the lists are empty by searching a -1 id ( == NULL)
            var wishlist = from s in _context.Wishlist where s.User_Id == gotuserId select s;
            var drones = from s in _context.Drones select s;
            var kabels = from s in _context.Kabels select s;
            var fotocameras = from s in _context.Fotocameras select s;
            var horloges = from s in _context.Horloges select s;
            var schoenen = from s in _context.Schoenen select s;
            var spelcomputers = from s in _context.Spelcomputers select s;
            var wrapper = new Categorie();
            wrapper.Kabels = kabels.ToList();
            wrapper.Drones = drones.ToList();
            wrapper.Spelcomputers = spelcomputers.ToList();
            wrapper.Horloges = horloges.ToList();
            wrapper.Fotocameras = fotocameras.ToList();
            wrapper.Schoenen = schoenen.ToList();
            wrapper.Wishlists = wishlist.ToList();
            return View(wrapper);
        }

        [Authorize]
        public async Task<IActionResult> AddToWishlist(int product, string model, int aantal, int prijs)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            //checks if the products already exists
            var check = from s in _context.Wishlist where s.Product_Id == product && s.Model_naam == model && s.User_Id == gotuserId select s;
            if (check.Count() == 0)
            {
                //creates the new Wishlist
                Wishlist m = new Wishlist
                {
                    User_Id = gotuserId,
                    Product_Id = product,
                    Model_naam = model,
                    Aantal = aantal,
                    Prijs = prijs,
                };

                _context.Wishlist.Add(m);
                _context.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                foreach (Wishlist Wishlist in check)
                {
                    Wishlist.Aantal = Wishlist.Aantal + aantal;
                }
                _context.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        [Authorize]
        public async Task<IActionResult> DeleteFromWishlist(int product, string model)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            //deleted a wishlist item
            var delete = await _context.Wishlist.SingleOrDefaultAsync(m => m.Product_Id == product && m.Model_naam == model && m.User_Id == gotuserId);
            _context.Wishlist.Remove(delete);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> DeleteAllFromWishlist(int product_id, string model)
        {
            //deleted a shoppingcart item
            var delete = _context.Wishlist.Where(m => m.Product_Id == product_id && m.Model_naam == model);
            _context.Wishlist.RemoveRange(delete);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int old_aantal, int new_aantal, string model)
        {
            // Query the database for the row to be updated.
            var query =
                from wishlist in _context.Wishlist
                where wishlist.Aantal == old_aantal && wishlist.Id == id && wishlist.Model_naam == model
                select wishlist;

            // Execute the query, and change the column values
            // you want to change.
            foreach (Wishlist Wishlist in query)
            {
                Wishlist.Aantal = new_aantal;
                // Insert any additional changes to column values.
            }

            // Submit the changes to the database.
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddToCart(int product, string model, int aantal, int prijs)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            //Get the current users cart
            var getwishlist = _context.Wishlist.Where(m => m.User_Id == gotuserId);
            //Loop all items from the cart in the OrderHistory model

            foreach (var item in getwishlist)
            {
                var check = from s in _context.Cart where s.Product_Id == item.Product_Id && s.Model_naam == item.Model_naam && s.User_Id == gotuserId select s;
                if (check.Count() == 0)
                {
                    Cart cart = new Cart
                    {
                        User_Id = item.User_Id,
                        Aantal = item.Aantal,
                        Model_naam = item.Model_naam,
                        Prijs = item.Prijs,
                        Product_Id = item.Product_Id
                    };
                    _context.Cart.Add(cart);
                    _context.Wishlist.RemoveRange(getwishlist);
                    await _context.SaveChangesAsync();
                }
                else
                {

                    check.First().Aantal = check.First().Aantal + item.Aantal;
                    _context.Wishlist.RemoveRange(getwishlist);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Cart", "Home");
        }

    }
}

