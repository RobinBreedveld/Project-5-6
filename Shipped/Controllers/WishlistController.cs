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
        public IActionResult Index()
        {
            
            return View();
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

                return RedirectToAction("Wishlist");
            }
            //goes to Wishlist if product is already added
            return RedirectToAction("Wishlist");
        }
       
    }
}

