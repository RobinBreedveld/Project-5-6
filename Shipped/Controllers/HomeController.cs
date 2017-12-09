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
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;

            // var file = "data.csv";
            // var sr = new StreamReader(file);
            // while (!sr.EndOfStream)
            // {
            //     var line = sr.ReadLine();
            //     var data = line.Split(new[] { ',' });
            //     var categorie = new Categorie() { Name = data[2], Image = data[1] };
            //     context.Categories.Add(categorie);
            // }

            // context.SaveChanges();

        }




//public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            //var categorieenproduct = _context.Categories.Include(p => p.Products);
            //await categorieenproduct.ToListAsync()
            return View();
        }
//  public async Task<IActionResult> Browse(int categorieId, int Id, string searchString, string sortOrder)
        public IActionResult Browse(string searchString)
        {
            ViewData["Message"] = "Your Browse page.";
            if( searchString == null ) {
                return View("Index");
            }
            else 
            {
            
            var kabels = _context.Kabels.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper() )); 
            var drones = _context.Drones.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper() ));
            var spelcomputers = _context.Spelcomputers.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper() ));
            var horloges = _context.Horloges.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper() ));
            var fotocameras = _context.Fotocameras.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper() ));
            var schoenen = _context.Schoenen.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper() ));
            var wrapper = new  Categorie();
            wrapper.Kabels = kabels.ToList();
            wrapper.Drones = drones.ToList();
            wrapper.Spelcomputers = spelcomputers.ToList();
            wrapper.Horloges = horloges.ToList();
            wrapper.Fotocameras = fotocameras.ToList();
            wrapper.Schoenen = schoenen.ToList();
            
            return View(wrapper);
            } 
        }
        [Authorize]
        public async Task<IActionResult> Cart(string model_name)
        {
             var claimsIdentity = (ClaimsIdentity)this.User.Identity;
             var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
             var gotuserId = claim.Value;
            // var kabel_cart_items = from kabel in _context.Kabels
            // from cart in _context.Cart
            // where kabel.Id == cart.Product_Id && cart.User_Id == gotuserId && cart.Model_naam == "Kabel"
            // select new Kabel{
            //         Naam = kabel.Naam,
            //         Id = kabel.Id
            // };
            // var drone_cart_items = from drones in _context.Drones
            //             from cart in _context.Cart
            //             where drones.Id == cart.Product_Id && cart.User_Id == gotuserId  && cart.Model_naam == "Drone"
            //             select new Drone{
            //                     Naam = drones.Naam,
            //                     Id = drones.Id
            //             };
            // var spelcomputer_cart_items = from spelcomputer in _context.Spelcomputers
            // from cart in _context.Cart
            // where spelcomputer.Id == cart.Product_Id && cart.User_Id == gotuserId && cart.Model_naam == "Spelcomputer"
            // select new Spelcomputer{
            //         Naam = spelcomputer.Naam,
            //         Id = spelcomputer.Id
            // };
            // var horloge_cart_items = from horloge in _context.Horloges
            // from cart in _context.Cart
            // where horloge.Id == cart.Product_Id && cart.User_Id == gotuserId && cart.Model_naam == "Horloge"
            // select new Horloge{
            //         Naam = horloge.Naam,
            //         Id = horloge.Id
            // };
            // var fotocamera_cart_items = from fotocamera in _context.Fotocameras
            // from cart in _context.Cart
            // where fotocamera.Id == cart.Product_Id && cart.User_Id == gotuserId && cart.Model_naam == "Fotocamera"
            // select new Fotocamera{
            //         Naam = fotocamera.Naam,
            //         Id = fotocamera.Id
            // };
            // var schoen_cart_items = from schoen in _context.Schoenen
            // from cart in _context.Cart
            // where schoen.Id == cart.Product_Id && cart.User_Id == gotuserId && cart.Model_naam == "Schoen"
            // select new Schoen{
            //         Naam = schoen.Naam,
            //         Id = schoen.Id
            // };
            // var wrappert = new  Categorie();
            // //add all the items to the wrapper
            // wrappert.Drones = drone_cart_items.ToList();
            // wrappert.Kabels = kabel_cart_items.ToList();
            // wrappert.Spelcomputers = spelcomputer_cart_items.ToList();
            // wrappert.Horloges = horloge_cart_items.ToList();
            // wrappert.Fotocameras = fotocamera_cart_items.ToList();
            // wrappert.Schoenen = schoen_cart_items.ToList();
             var cart_items = from s in _context.Cart where s.User_Id == gotuserId select s;
            return View(cart_items);
        }
        [Authorize]
        public async Task<IActionResult> AddToShoppingCart(int product, string model, int aantal) {
           var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            //checks if the products already exists
            var check = from s in _context.Cart where s.Product_Id == product && s.Model_naam == model && s.User_Id == gotuserId select s;
            if (check.Count() == 0){
                //creates the new cart
                Cart m = new Cart {
                    User_Id = gotuserId,
                    Product_Id = product,
                    Model_naam = model,
                    Aantal = aantal
                };

                _context.Cart.Add(m);
                _context.SaveChanges();

                return RedirectToAction("Cart");
            }
            //goes to cart if product is already added
            return RedirectToAction("Cart");
        }
        [Authorize]
        public async Task<IActionResult> DeleteFromShoppingCart(int product, string model) {
           var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            //deleted a shoppingcart item
            var delete = await _context.Cart.SingleOrDefaultAsync(m => m.Product_Id == product && m.Model_naam == model && m.User_Id == gotuserId);
            _context.Cart.Remove(delete);
            await _context.SaveChangesAsync();
            return RedirectToAction("Cart");
        }
         [Authorize]
        public async Task<IActionResult> DeleteAllFromShoppingCart(int product_id, string model) {
            //deleted a shoppingcart item
            var delete = _context.Cart.Where(m => m.Product_Id == product_id && m.Model_naam == model);      
            _context.Cart.RemoveRange(delete);
            return RedirectToAction("Cart");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,int old_aantal, int new_aantal, string model)
        {
                    // Query the database for the row to be updated.
        var query =
            from cart in _context.Cart
            where cart.Aantal == old_aantal && cart.Id == id && cart.Model_naam == model
            select cart;

        // Execute the query, and change the column values
        // you want to change.
        foreach (Cart cart in query)
        {
            cart.Aantal = new_aantal;
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
            return RedirectToAction("Cart");
        }
         private bool cartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
    }
}

