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
using Newtonsoft.Json;


namespace login2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context, IEmailSender emailSender, UserManager<ApplicationUser> manager)

        {

            _manager = manager;
            _emailSender = emailSender;
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
            //meestgekochtekabel         
            var meestgekochtekabel = _context.Kabels.Max( p => p.Aantal_gekocht);
            var getkabel = _context.Kabels.Where( p => p.Aantal_gekocht == meestgekochtekabel);

            //meestgekochtedrone
            var meestgekochtedrone = _context.Drones.Max( p => p.Aantal_gekocht);
            var getdrone = _context.Drones.Where( p => p.Aantal_gekocht == meestgekochtedrone);

            //meestgekochtefotocamera
            var meestgekochtefotocamera = _context.Fotocameras.Max( p => p.Aantal_gekocht);
            var getfotocamera = _context.Fotocameras.Where( p => p.Aantal_gekocht == meestgekochtefotocamera);

            //meestgekochtehorloge
            var meestgekochtehorloge = _context.Horloges.Max( p => p.Aantal_gekocht);
            var gethorloge = _context.Horloges.Where( p => p.Aantal_gekocht == meestgekochtehorloge);

            //meestgekochteschoen
            var meestgekochteschoen = _context.Schoenen.Max( p => p.Aantal_gekocht);
            var getschoen = _context.Schoenen.Where( p => p.Aantal_gekocht == meestgekochteschoen);

            //meestgekochtespelcomputer
            var meestgekochtespelcomputer = _context.Spelcomputers.Max( p => p.Aantal_gekocht);
            var getspelcomputer = _context.Spelcomputers.Where( p => p.Aantal_gekocht == meestgekochtespelcomputer);

            var wrapper = new Categorie();
            
            wrapper.Kabels = getkabel.ToList();
            wrapper.Drones = getdrone.ToList();
            wrapper.Fotocameras = getfotocamera.ToList();
            wrapper.Horloges = gethorloge.ToList();
            wrapper.Schoenen = getschoen.ToList();
            wrapper.Spelcomputers = getspelcomputer.ToList();

            return View(wrapper);
        }
        //  public async Task<IActionResult> Browse(int categorieId, int Id, string searchString, string sortOrder)
        public IActionResult Browse(string searchString)
        {
            ViewData["Message"] = "Your Browse page .";
            if (searchString == null)
            {
                return View("Index");
            }
            else
            {
                var kabelresultaat = _context.Kabels.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper()) || searchString.Contains("kabel"));
                var droneresultaat = _context.Drones.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper()) || searchString.Contains("drone"));
                var spelcomputerresultaat = _context.Spelcomputers.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper()) || searchString.Contains("spelcomputer"));
                var horlogeresultaat = _context.Horloges.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper()) || searchString.Contains("horloge"));
                var fotocameraresultaat = _context.Fotocameras.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper()) || searchString.Contains("fotocamera"));
                var schoenresultaat = _context.Schoenen.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper()) || searchString.Contains("schoen"));
                
                var wrapper = new Categorie();
                
                wrapper.Kabels = kabelresultaat.ToList();
                wrapper.Drones = droneresultaat.ToList();
                wrapper.Spelcomputers = spelcomputerresultaat.ToList();
                wrapper.Horloges = horlogeresultaat.ToList();
                wrapper.Fotocameras = fotocameraresultaat.ToList();
                wrapper.Schoenen = schoenresultaat.ToList();

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
            var totaal = from item in _context.Cart
                         where item.User_Id == gotuserId
                         group item by item.User_Id into items
                         select new
                         {
                             Totaal = items.Sum(x => x.Prijs * x.Aantal)
                         };
                         
            foreach (var item in totaal)
            {
                ViewBag.Totaal = item.Totaal;
            }
            return View(cart_items);
        }

        [Authorize]
        public async Task<IActionResult> AddToShoppingCart(int product, string model, int aantal, int prijs)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            //checks if the products already exists
            var check = from s in _context.Cart where s.Product_Id == product && s.Model_naam == model && s.User_Id == gotuserId select s;
            if (check.Count() == 0)
            {
                //creates the new cart
                Cart m = new Cart
                {
                    User_Id = gotuserId,
                    Product_Id = product,
                    Model_naam = model,
                    Aantal = aantal,
                    Prijs = prijs,
                };

                _context.Cart.Add(m);
                _context.SaveChanges();

                return RedirectToAction("Cart");
            }
            //goes to cart if product is already added
            return RedirectToAction("Cart");
        }

        [Authorize]
        public async Task<IActionResult> DeleteFromShoppingCart(int product, string model)
        {
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
        public async Task<IActionResult> DeleteAllFromShoppingCart(int product_id, string model)
        {
            //deleted a shoppingcart item
            var delete = _context.Cart.Where(m => m.Product_Id == product_id && m.Model_naam == model);
            _context.Cart.RemoveRange(delete);
            return RedirectToAction("Cart");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int old_aantal, int new_aantal, string model)
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

        [Authorize]
        public async Task<IActionResult> Buy()
        {

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            //Get the current users cart
            var getcart = _context.Cart.Where(m => m.User_Id == gotuserId);
            string product = "";

            int totaalprijs = 0;
            Boolean empty = true;
            //Loop all items from the cart in the OrderHistory model
            foreach (var item in getcart)
            {

                string items;
                items = item + item.Model_naam;
                OrderHistory order = new OrderHistory
                {
                    User_Id = gotuserId,
                    Product_Id = item.Product_Id,
                    Model_naam = item.Model_naam,
                    Prijs = item.Prijs,
                    Order_nummer = _context.OrderHistory.Count().ToString()

                };

                switch (item.Model_naam)
                {
                    case "Kabel":
                        var getkabel = _context.Kabels.Where(m => m.Id == item.Product_Id && item.Model_naam == "Kabel");
                        getkabel.First().Aantal = getkabel.First().Aantal - item.Aantal;
                        getkabel.First().Aantal_gekocht = getkabel.First().Aantal_gekocht + item.Aantal;
                        product = product + getkabel.First().Naam + ",";
                        totaalprijs = totaalprijs + getkabel.First().Prijs;
                        empty = false;
                        break;
                    case "Drone":
                        var getdrone = _context.Drones.Where(m => m.Id == item.Product_Id && item.Model_naam == "Drone");
                        getdrone.First().Aantal = getdrone.First().Aantal - item.Aantal;
                        getdrone.First().Aantal_gekocht = getdrone.First().Aantal_gekocht + item.Aantal;
                        product = product + getdrone.First().Naam + ",";
                        totaalprijs = totaalprijs + getdrone.First().Prijs;
                        empty = false;

                        break;
                    case "Spelcomputer":
                        var getspelcomputer = _context.Spelcomputers.Where(m => m.Id == item.Product_Id && item.Model_naam == "Spelcomputer");
                        getspelcomputer.First().Aantal = getspelcomputer.First().Aantal - item.Aantal;
                        getspelcomputer.First().Aantal_gekocht = getspelcomputer.First().Aantal_gekocht + item.Aantal;
                        product = product + "," + getspelcomputer.First().Naam;
                        totaalprijs = totaalprijs + getspelcomputer.First().Prijs;
                        empty = false;
                        break;
                    case "Horloge":
                        var gethorloge = _context.Horloges.Where(m => m.Id == item.Product_Id && item.Model_naam == "Horloge");
                        gethorloge.First().Aantal = gethorloge.First().Aantal - item.Aantal;
                        gethorloge.First().Aantal_gekocht = gethorloge.First().Aantal_gekocht + item.Aantal;
                        product = product + gethorloge.First().Naam + ",";
                        totaalprijs = totaalprijs + gethorloge.First().Prijs;
                        empty = false;

                        break;
                    case "Fotocamera":
                        var getfotocamera = _context.Fotocameras.Where(m => m.Id == item.Product_Id && item.Model_naam == "Fotocamera");
                        getfotocamera.First().Aantal = getfotocamera.First().Aantal - item.Aantal;
                        getfotocamera.First().Aantal_gekocht = getfotocamera.First().Aantal_gekocht + item.Aantal;
                        product = product + getfotocamera.First().Naam + ",";
                        totaalprijs = totaalprijs + getfotocamera.First().Prijs;
                        empty = false;

                        break;
                    case "Schoen":
                        var getschoen = _context.Schoenen.Where(m => m.Id == item.Product_Id && item.Model_naam == "Schoen");
                        getschoen.First().Aantal = getschoen.First().Aantal - item.Aantal;
                        getschoen.First().Aantal_gekocht = getschoen.First().Aantal_gekocht + item.Aantal;
                        product = product + getschoen.First().Naam + ",";
                        totaalprijs = totaalprijs + getschoen.First().Prijs;
                        empty = false;

                        break;
                    default:
                        Console.WriteLine("Error");
                        break;
                }
                //Product Aantal - Cart Aantal    
                _context.OrderHistory.Add(order);
                _context.Cart.RemoveRange(getcart);


            }
            if (empty == false)
            {
                await _emailSender.SendEmailAsync($"{claimsIdentity.Name}", $"Purchase confirmation of order  ", $"Your order with order  has been confirmd and will be send! {product} <br/> Totaal prijs: {totaalprijs}");
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Cart");

        }



        [Authorize]
        public IActionResult OrderHistory()
        {

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            var OrderHistory = from m in _context.OrderHistory
                               where m.User_Id == gotuserId
                               select m;
            return View(OrderHistory);
        }

        [Authorize]
        public async Task<IActionResult> OrderHistoryItem(string order_nummer)
        {
            if (order_nummer == null)
            {
                return View("Index");
            }
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            var ordersinid = _context.OrderHistory.Where(m => m.Order_nummer == order_nummer && m.User_Id == gotuserId);
            if (ordersinid == null)
            {
                return NotFound();
            }
            return View(ordersinid);
        }
        public IActionResult Statistics()
        {
            ViewBag.ChartData = "Value,Value1,Value2,Value3";
            ViewBag.ChartLabels = "Test,Test1,Test2,Test3";
            return View();
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

