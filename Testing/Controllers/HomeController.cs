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
            
            var kabels = _context.Kabels.Where(p => p.Naam.StartsWith(searchString.ToLower())); 
            var drones = _context.Drones.Where(p => p.Naam.StartsWith(searchString.ToLower()));
            var spelcomputers = _context.Spelcomputers.Where(p => p.Naam.StartsWith(searchString.ToUpper()));
            var horloges = _context.Horloges.Where(p => p.Naam.StartsWith(searchString.ToUpper()));
            var fotocameras = _context.Fotocameras.Where(p => p.Naam.StartsWith(searchString.ToUpper()));
            var schoenen = _context.Schoenen.Where(p => p.Naam.StartsWith(searchString.ToUpper()));
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
        public async Task<IActionResult> Cart(string model_name)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            var kabel_cart_items = from kabel in _context.Kabels
            from cart in _context.Cart
            where kabel.Id == cart.Product_Id && cart.User_Id == gotuserId 
            select new Kabel{
                    Naam = kabel.Naam,
                    Id = kabel.Id
            };
            var drone_cart_items = from drones in _context.Drones
                        from cart in _context.Cart
                        where drones.Id == cart.Product_Id && cart.User_Id == gotuserId 
                        select new Drone{
                                Naam = drones.Naam,
                                Id = drones.Id
                        };
            var spelcomputer_cart_items = from spelcomputer in _context.Spelcomputers
            from cart in _context.Cart
            where spelcomputer.Id == cart.Product_Id && cart.User_Id == gotuserId 
            select new Spelcomputer{
                    Naam = spelcomputer.Naam,
                    Id = spelcomputer.Id
            };
            var horloge_cart_items = from horloge in _context.Horloges
            from cart in _context.Cart
            where horloge.Id == cart.Product_Id && cart.User_Id == gotuserId 
            select new Horloge{
                    Naam = horloge.Naam,
                    Id = horloge.Id
            };
            var fotocamera_cart_items = from fotocamera in _context.Fotocameras
            from cart in _context.Cart
            where fotocamera.Id == cart.Product_Id && cart.User_Id == gotuserId 
            select new Fotocamera{
                    Naam = fotocamera.Naam,
                    Id = fotocamera.Id
            };
            var schoen_cart_items = from schoen in _context.Schoenen
            from cart in _context.Cart
            where schoen.Id == cart.Product_Id && cart.User_Id == gotuserId 
            select new Schoen{
                    Naam = schoen.Naam,
                    Id = schoen.Id
            };
            var wrappert = new  Categorie();
            //add all the items to the wrapper
            wrappert.Drones = drone_cart_items.ToList();
            wrappert.Kabels = kabel_cart_items.ToList();
            wrappert.Spelcomputers = spelcomputer_cart_items.ToList();
            wrappert.Horloges = horloge_cart_items.ToList();
            wrappert.Fotocameras = fotocamera_cart_items.ToList();
            wrappert.Schoenen = schoen_cart_items.ToList();

            return View(wrappert);
        }
        [Authorize]
        public async Task<IActionResult> AddToShoppingCart(int product, string model) {
           var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            Cart m = new Cart {
                User_Id = gotuserId,
                Product_Id = product,
                Model_naam = model
            };
            _context.Cart.Add(m);
            _context.SaveChanges();
            return RedirectToAction("Cart");
        }
        [Authorize]
        public async Task<IActionResult> DeleteFromShoppingCart(int product, string model) {
           var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            var delete = await _context.Cart.SingleOrDefaultAsync(m => m.Product_Id == product && m.Model_naam == model && m.User_Id == gotuserId);
            _context.Cart.Remove(delete);
            await _context.SaveChangesAsync();
            return RedirectToAction("Cart");
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

