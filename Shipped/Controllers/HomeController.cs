﻿using System;
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
        public async Task<IActionResult> Index(string added)
        {
            //meestgekochtekabel       
            var countkabel = _context.Kabels.Count();
            var getkabel = await _context.Kabels.ToListAsync();  
            if (countkabel > 0){
            var meestgekochtekabel = _context.Kabels.Max(p => p.Aantal_gekocht);
            getkabel = await _context.Kabels.Where(p => p.Aantal_gekocht == meestgekochtekabel).ToListAsync();
            }

            //meestgekochtedrone
            var countdrone = _context.Drones.Count();
            var getdrone = await _context.Drones.ToListAsync();  
            if (countdrone > 0){
            var meestgekochtedrone = _context.Drones.Max(p => p.Aantal_gekocht);
            getdrone =  await _context.Drones.Where(p => p.Aantal_gekocht == meestgekochtedrone).ToListAsync();
            }
            //meestgekochtefotocamera
            var countfotocamera = _context.Fotocameras.Count();
            var getfotocamera = await _context.Fotocameras.ToListAsync();  
            if (countfotocamera > 0){
            var meestgekochtefotocamera = _context.Fotocameras.Max(p => p.Aantal_gekocht);
            getfotocamera = await _context.Fotocameras.Where(p => p.Aantal_gekocht == meestgekochtefotocamera).ToListAsync();
            }

            //meestgekochtehorloge
            var counthorloge = _context.Horloges.Count();
            var gethorloge = await _context.Horloges.ToListAsync();  
            if (counthorloge> 0){
            var meestgekochtehorloge = _context.Horloges.Max(p => p.Aantal_gekocht);
            gethorloge = await _context.Horloges.Where(p => p.Aantal_gekocht == meestgekochtehorloge).ToListAsync();
            }

            //meestgekochteschoen
            var countschoen = _context.Schoenen.Count();
            var getschoen = await _context.Schoenen.ToListAsync();  
            if (countschoen> 0){
            var meestgekochteschoen = _context.Schoenen.Max(p => p.Aantal_gekocht);
            getschoen = await _context.Schoenen.Where(p => p.Aantal_gekocht == meestgekochteschoen).ToListAsync(); 
            }

            //meestgekochtespelcomputer
            var countspelcomputer = _context.Spelcomputers.Count();
            var getspelcomputer = await _context.Spelcomputers.ToListAsync();  
            if (countspelcomputer> 0){
            var meestgekochtespelcomputer = _context.Spelcomputers.Max(p => p.Aantal_gekocht);
            getspelcomputer = await _context.Spelcomputers.Where(p => p.Aantal_gekocht == meestgekochtespelcomputer).ToListAsync();
            }

            var wrapper = new Categorie();

            wrapper.Kabels = getkabel.ToList();
            wrapper.Drones = getdrone.ToList();
            wrapper.Fotocameras = getfotocamera.ToList();
            wrapper.Horloges = gethorloge.ToList();
            wrapper.Schoenen = getschoen.ToList();
            wrapper.Spelcomputers = getspelcomputer.ToList();
            ViewBag.AddToCart = added;

            return View(wrapper);
        }
        //  public async Task<IActionResult> Browse(int categorieId, int Id, string searchString, string sortOrder)
        public IActionResult Browse(string searchString)
        {
            ViewData["Message"] = "Your Browse page .";
            if (searchString == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var kabelresultaat = _context.Kabels.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper()) || "kabels".ToString().ToUpper().StartsWith(searchString.ToUpper()));
                var droneresultaat = _context.Drones.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper()) || "drones".ToString().ToUpper().StartsWith(searchString.ToUpper()));
                var spelcomputerresultaat = _context.Spelcomputers.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper()) || "spelcomputers".ToString().ToUpper().StartsWith(searchString.ToUpper()));
                var horlogeresultaat = _context.Horloges.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper()) || "horloges".ToString().ToUpper().StartsWith(searchString.ToUpper()));
                var fotocameraresultaat = _context.Fotocameras.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper()) || "fotocameras".ToString().ToUpper().StartsWith(searchString.ToUpper()));
                var schoenresultaat = _context.Schoenen.Where(p => p.Naam.ToUpper().StartsWith(searchString.ToUpper()) || p.Merk.ToUpper().StartsWith(searchString.ToUpper()) || p.Type.ToUpper().StartsWith(searchString.ToUpper()) || "schoenen".ToString().ToUpper().StartsWith(searchString.ToUpper()));

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
        public async Task<IActionResult> Cart(string popup)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            var cart_items = from s in _context.Cart where s.User_Id == gotuserId select s;
            var drones = from s in _context.Drones select s;
            var kabels = from s in _context.Kabels select s;
            var fotocameras = from s in _context.Fotocameras select s;
            var horloges = from s in _context.Horloges select s;
            var schoenen = from s in _context.Schoenen select s;
            var spelcomputers = from s in _context.Spelcomputers select s;

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
            var wrapper = new Categorie();
            wrapper.Carts = cart_items.ToList();
            wrapper.Drones = drones.ToList();
            wrapper.Kabels = kabels.ToList();
            wrapper.Fotocameras = fotocameras.ToList();
            wrapper.Horloges = horloges.ToList();
            wrapper.Schoenen = schoenen.ToList();
            wrapper.Spelcomputers = spelcomputers.ToList();
            ViewBag.Popup = popup;
            return View(wrapper);
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
                return Redirect(Request.Headers["Referer"].ToString());
                // return RedirectToAction("Cart");
            }
            else
            {
                foreach (Cart cart in check)
                {
                    cart.Aantal = cart.Aantal + aantal;
                }
                _context.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
                // return RedirectToAction("Cart");
            }
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

            var getordernummer = _context.OrderHistory.Where(m => m.User_Id == gotuserId);
            string product = "";

            int totaalprijs = 0;
            Boolean empty = true;

            var voorraadproduct = "";
            var naamproduct = "";
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
                    Aantal = item.Aantal,
                    Status = "Besteld",
                    Order_nummer = _context.OrderHistory.Count().ToString()

                };

                switch (item.Model_naam)
                {
                    case "Kabel":
                        var getkabel = _context.Kabels.Where(m => m.Id == item.Product_Id && item.Model_naam == "Kabel");
                        voorraadproduct = getkabel.First().Aantal.ToString();
                        naamproduct = getkabel.First().Naam.ToString();
                        var checkkabel = getkabel.First().Aantal - item.Aantal;

                        if (checkkabel >= 0)
                        {
                            getkabel.First().Aantal = getkabel.First().Aantal - item.Aantal;
                            getkabel.First().Aantal_gekocht = getkabel.First().Aantal_gekocht + item.Aantal;
                            product = product + getkabel.First().Naam + ",";
                            totaalprijs = totaalprijs + getkabel.First().Prijs;
                            empty = false;
                        }
                        else
                        {
                            empty = true;
                        }
                        break;
                    case "Drone":
                        var getdrone = _context.Drones.Where(m => m.Id == item.Product_Id && item.Model_naam == "Drone");
                        voorraadproduct = getdrone.First().Aantal.ToString();
                        naamproduct = getdrone.First().Naam.ToString();
                        var checkdrone = getdrone.First().Aantal - item.Aantal;

                        if (checkdrone >= 0)
                        {
                            getdrone.First().Aantal = getdrone.First().Aantal - item.Aantal;
                            getdrone.First().Aantal_gekocht = getdrone.First().Aantal_gekocht + item.Aantal;
                            product = product + getdrone.First().Naam + ",";
                            totaalprijs = totaalprijs + getdrone.First().Prijs;
                            empty = false;
                        }
                        else
                        {
                            empty = true;
                        }
                        break;
                    case "Spelcomputer":
                        var getspelcomputer = _context.Spelcomputers.Where(m => m.Id == item.Product_Id && item.Model_naam == "Spelcomputer");
                        voorraadproduct = getspelcomputer.First().Aantal.ToString();
                        naamproduct = getspelcomputer.First().Naam.ToString();
                        var checkspelcomputer = getspelcomputer.First().Aantal - item.Aantal;

                        if (checkspelcomputer >= 0)
                        {
                            getspelcomputer.First().Aantal = getspelcomputer.First().Aantal - item.Aantal;
                            getspelcomputer.First().Aantal_gekocht = getspelcomputer.First().Aantal_gekocht + item.Aantal;
                            product = product + "," + getspelcomputer.First().Naam;
                            totaalprijs = totaalprijs + getspelcomputer.First().Prijs;
                            empty = false;
                        }
                        else
                        {
                            empty = true;
                        }
                        break;
                    case "Horloge":
                        var gethorloge = _context.Horloges.Where(m => m.Id == item.Product_Id && item.Model_naam == "Horloge");
                        voorraadproduct = gethorloge.First().Aantal.ToString();
                        naamproduct = gethorloge.First().Naam.ToString();
                        var checkhorloge = gethorloge.First().Aantal - item.Aantal;

                        if (checkhorloge >= 0)
                        {
                            gethorloge.First().Aantal = gethorloge.First().Aantal - item.Aantal;
                            gethorloge.First().Aantal_gekocht = gethorloge.First().Aantal_gekocht + item.Aantal;
                            product = product + gethorloge.First().Naam + ",";
                            totaalprijs = totaalprijs + gethorloge.First().Prijs;
                            empty = false;
                        }
                        else
                        {
                            empty = true;
                        }
                        break;
                    case "Fotocamera":
                        var getfotocamera = _context.Fotocameras.Where(m => m.Id == item.Product_Id && item.Model_naam == "Fotocamera");
                        voorraadproduct = getfotocamera.First().Aantal.ToString();
                        naamproduct = getfotocamera.First().Naam.ToString();
                        var checkfotocamera = getfotocamera.First().Aantal - item.Aantal;
                        if (checkfotocamera >= 0)
                        {
                            getfotocamera.First().Aantal = getfotocamera.First().Aantal - item.Aantal;
                            getfotocamera.First().Aantal_gekocht = getfotocamera.First().Aantal_gekocht + item.Aantal;
                            product = product + getfotocamera.First().Naam + ",";
                            totaalprijs = totaalprijs + getfotocamera.First().Prijs;
                            empty = false;
                        }
                        else
                        {
                            empty = true;
                        }
                        break;
                    case "Schoen":
                        var getschoen = _context.Schoenen.Where(m => m.Id == item.Product_Id && item.Model_naam == "Schoen");
                        naamproduct = getschoen.First().Naam.ToString();
                        voorraadproduct = getschoen.First().Aantal.ToString();
                        var checkschoen = getschoen.First().Aantal - item.Aantal;
                        if (checkschoen >= 0)
                        {
                            getschoen.First().Aantal = getschoen.First().Aantal - item.Aantal;
                            getschoen.First().Aantal_gekocht = getschoen.First().Aantal_gekocht + item.Aantal;
                            product = product + getschoen.First().Naam + ",";
                            totaalprijs = totaalprijs + getschoen.First().Prijs;
                            empty = false;
                        }
                        else
                        {
                            empty = true;
                        }
                        break;
                    default:
                        Console.WriteLine("Error");
                        break;
                }
                if (empty == false)
                {
                    _context.OrderHistory.Add(order);
                }
                else
                {
                    return RedirectToAction("Cart", new { popup = "De voorraad van " + naamproduct + " is helaas " + voorraadproduct + ", bestel een kleiner aantal van dit product!" });
                }
            }
            if (empty == false)
            {
                var ordernummer = _context.OrderHistory.Count().ToString();                
                _context.Cart.RemoveRange(getcart);
                await _context.SaveChangesAsync();
                var totaal = from item in _context.OrderHistory
                         where item.User_Id == gotuserId && item.Order_nummer == ordernummer
                         group item by item.User_Id into items
                         select new
                         {
                             Totaal = items.Sum(x => x.Prijs * x.Aantal)
                         };
                var realtotaal = 0;
                foreach(var item in totaal) {
                    realtotaal = item.Totaal;
                }
                await _emailSender.SendEmailAsync($"{claimsIdentity.Name}", $"Aankoopbevestiging van order: {ordernummer}  ", $"Beste meneer/mevrouw, <br> <br>Uw order met order nummer {ordernummer}  is bevestigd en zal verzonden worden! <br> <br> Besteld product: {product} <br/> Totaal prijs: {realtotaal} <br> <br> Met vriendelijke groet, <br> Shipped.nl");                
                return RedirectToAction("Cart", new { popup = "Uw bestelling is met succes geplaatst! Bekijk uw mail of de ordergeschiedenis voor meer informatie!" });
            }
            return RedirectToAction("Cart", new { popup = "De voorraad van " + naamproduct + "is " + voorraadproduct + ", koop een kleiner aantal van dit product" });
        }



        [Authorize]
        public IActionResult OrderHistory()
        {

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            var OrderHistory = _context.OrderHistory.GroupBy(p => new { p.Order_nummer })
                            .Where(p => p.First().User_Id == gotuserId)
                            .Select(g => g.First())
                            .OrderBy(v => v.Id)
                            .ToList();
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
            var drones = from s in _context.Drones select s;
            var kabels = from s in _context.Kabels select s;
            var fotocameras = from s in _context.Fotocameras select s;
            var horloges = from s in _context.Horloges select s;
            var schoenen = from s in _context.Schoenen select s;
            var spelcomputers = from s in _context.Spelcomputers select s;

            var totaal = from item in _context.OrderHistory
                         where item.User_Id == gotuserId && item.Order_nummer == order_nummer
                         group item by item.User_Id into items
                         select new
                         {
                             Totaal = items.Sum(x => x.Prijs * x.Aantal)
                         };

            foreach (var item in totaal)
            {
                ViewBag.Totaal = item.Totaal;
            }
            if (ordersinid == null)
            {
                return NotFound();
            }
            var wrapper = new Categorie();
            wrapper.OrderHistory = ordersinid.ToList();
            wrapper.Drones = drones.ToList();
            wrapper.Kabels = kabels.ToList();
            wrapper.Fotocameras = fotocameras.ToList();
            wrapper.Horloges = horloges.ToList();
            wrapper.Schoenen = schoenen.ToList();
            wrapper.Spelcomputers = spelcomputers.ToList();
            return View(wrapper);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Statistics()
        {
            //Chart 1 & 2
            //A list with all aantalverkocht values in it. | Main list 
            List<int> allverkocht = new List<int>();
            //Queries to get the total of aantalverkocht per categorie | Queries categories
            var getkabels = from a in _context.Kabels group a by a.Aantal_gekocht into items select new { Totaal = items.Sum(x => x.Aantal_gekocht) };
            var getschoenen = from a in _context.Schoenen group a by a.Aantal_gekocht into items select new { Totaal = items.Sum(x => x.Aantal_gekocht) };
            var getdrones = from a in _context.Drones group a by a.Aantal_gekocht into items select new { Totaal = items.Sum(x => x.Aantal_gekocht) };
            var getspelcomputer = from a in _context.Spelcomputers group a by a.Aantal_gekocht into items select new { Totaal = items.Sum(x => x.Aantal_gekocht) };
            var gethorloges = from a in _context.Horloges group a by a.Aantal_gekocht into items select new { Totaal = items.Sum(x => x.Aantal_gekocht) };
            var getfotocameras = from a in _context.Fotocameras group a by a.Aantal_gekocht into items select new { Totaal = items.Sum(x => x.Aantal_gekocht) };
            //Foreach loops that create a total value per categorie | Loops total sold
            //Kabels
            int aantalkabels = 0;
            foreach (var item in getkabels) { aantalkabels = aantalkabels + item.Totaal; }
            //Schoenen
            int aantalschoenen = 0;
            foreach (var item in getschoenen) { aantalschoenen = aantalschoenen + item.Totaal; }
            //Drones
            int aantaldrones = 0;
            foreach (var item in getdrones) { aantaldrones = aantaldrones + item.Totaal; }
            //Spelcomputers
            int aantalspelcomputers = 0;
            foreach (var item in getspelcomputer)
            { aantalspelcomputers = aantalspelcomputers + item.Totaal; }
            //Horloges
            int aantalhorloges = 0;
            foreach (var item in gethorloges) { aantalhorloges = aantalhorloges + item.Totaal; }
            //Fotocameras
            int aantalfotocameras = 0;
            foreach (var item in getfotocameras) { aantalfotocameras = aantalfotocameras + item.Totaal; }
            //Adds the total value to the main list | Creating main list
            allverkocht.Add(aantalkabels);
            allverkocht.Add(aantalschoenen);
            allverkocht.Add(aantaldrones);
            allverkocht.Add(aantalspelcomputers);
            allverkocht.Add(aantalhorloges);
            allverkocht.Add(aantalfotocameras);
            //Sends the main list to the view | Returning main list
            ViewBag.RepAll = allverkocht;
            //Chart 3 
            //A list with all aantalverkocht values in it. | Main list 
            List<int> allvoorraad = new List<int>();
            //Queries to get the total of aantalverkocht per categorie | Queries categories
            var getvoorraadkabels = from a in _context.Kabels group a by a.Aantal into items select new { Totaal = items.Sum(x => x.Aantal) };
            var getvoorraadschoenen = from a in _context.Schoenen group a by a.Aantal into items select new { Totaal = items.Sum(x => x.Aantal) };
            var getvoorraaddrones = from a in _context.Drones group a by a.Aantal into items select new { Totaal = items.Sum(x => x.Aantal) };
            var getvoorraadspelcomputer = from a in _context.Spelcomputers group a by a.Aantal into items select new { Totaal = items.Sum(x => x.Aantal) };
            var getvoorraadhorloges = from a in _context.Horloges group a by a.Aantal into items select new { Totaal = items.Sum(x => x.Aantal) };
            var getvoorraadfotocameras = from a in _context.Fotocameras group a by a.Aantal into items select new { Totaal = items.Sum(x => x.Aantal) };
            //Foreach loops that create a total value per categorie | Loops total sold
            //Kabels
            int voorraadkabels = 0;
            foreach (var item in getvoorraadkabels) { voorraadkabels = voorraadkabels + item.Totaal; }
            //Schoenen
            int voorraadschoenen = 0;
            foreach (var item in getvoorraadschoenen) { voorraadschoenen = voorraadschoenen + item.Totaal; }
            //Drones
            int voorraaddrones = 0;
            foreach (var item in getvoorraaddrones) { voorraaddrones = voorraaddrones + item.Totaal; }
            //Spelcomputers
            int voorraadspelcomputers = 0;
            foreach (var item in getvoorraadspelcomputer)
            { voorraadspelcomputers = voorraadspelcomputers + item.Totaal; }
            //Horloges
            int voorraadhorloges = 0;
            foreach (var item in getvoorraadhorloges) { voorraadhorloges = voorraadhorloges + item.Totaal; }
            //Fotocameras
            int voorraadfotocameras = 0;
            foreach (var item in getvoorraadfotocameras) { voorraadfotocameras = voorraadfotocameras + item.Totaal; }
            //Adds the total value to the main list | Creating main list
            allvoorraad.Add(voorraadkabels);
            allvoorraad.Add(voorraadschoenen);
            allvoorraad.Add(voorraaddrones);
            allvoorraad.Add(voorraadspelcomputers);
            allvoorraad.Add(voorraadhorloges);
            allvoorraad.Add(voorraadfotocameras);
            //Sends the main list to the view | Returning main list
            ViewBag.RepVoorraad = allvoorraad;


            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Orders()
        {

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            var OrderHistory = _context.OrderHistory.GroupBy(p => new { p.Order_nummer })
                            .Select(g => g.First())
                            .OrderBy(v => v.Id)
                            .ToList();
            return View(OrderHistory);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OrderItem(string order_nummer)
        {
            if (order_nummer == null)
            {
                return View("Index");
            }
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var gotuserId = claim.Value;
            var ordersinid = _context.OrderHistory.Where(m => m.Order_nummer == order_nummer);
            var drones = from s in _context.Drones select s;
            var kabels = from s in _context.Kabels select s;
            var fotocameras = from s in _context.Fotocameras select s;
            var horloges = from s in _context.Horloges select s;
            var schoenen = from s in _context.Schoenen select s;
            var spelcomputers = from s in _context.Spelcomputers select s;

            var totaal = from item in _context.OrderHistory
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
            if (ordersinid == null)
            {
                return NotFound();
            }
            var wrapper = new Categorie();
            wrapper.OrderHistory = ordersinid.ToList();
            wrapper.Drones = drones.ToList();
            wrapper.Kabels = kabels.ToList();
            wrapper.Fotocameras = fotocameras.ToList();
            wrapper.Horloges = horloges.ToList();
            wrapper.Schoenen = schoenen.ToList();
            wrapper.Spelcomputers = spelcomputers.ToList();
            return View(wrapper);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrder(int id, string old_status, string new_status, string model, string user_id)
        {
            // Query the database for the row to be updated.
            var queryorder =
                from order in _context.OrderHistory
                where order.Id == id && order.Model_naam == model && order.User_Id == user_id
                select order;

            // Execute the query, and change the column values
            // you want to change.
            foreach (OrderHistory order in queryorder)
            {
                order.Status = new_status;
                // Insert any additional changes to column values.
            }

            // Submit the changes to the database.
            try
            {

                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var userName = claimsIdentity.Name;
                var status = queryorder.First().Status;
                var userfound = from users in _context.Users where users.Id == user_id select users.NormalizedEmail;
                var oneuser = userfound.First().ToString();

                await _emailSender.SendEmailAsync($"{oneuser}", "Order status", $"Beste meneer/mevrouw, <br/> Uw status is veranderd naar {status} <br> <br> Met vriendelijke groet, <br> <br> Shipped.nl");

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }
            return RedirectToAction("OrderItem", new { order_nummer = queryorder.First().Order_nummer });
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
        public IActionResult ContactSucces()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult SendContactMail(string name, string email, string subject, string description)
        {
            
            _emailSender.SendEmailAsync("project3informatica@gmail.com", $"{subject}", $"Naam: {name} <br/> Email: {email} <br/> Onderwerp: {subject} <br/> Omschrijving: <br/> {description} ");

            return RedirectToAction("ContactSucces");
        }
    }
}

