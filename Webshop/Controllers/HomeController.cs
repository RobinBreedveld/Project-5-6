using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webshop.Models;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace Webshop.Controllers
{
    public class HomeController : Controller
    {
        ProductContext storeDB = new ProductContext();
        public IActionResult Index()
        {
            var categories = storeDB.Categories.ToList();
            return View(categories);
        }
        public IActionResult Browse(int categorieId, int Id, string searchString)
        {
            ViewData["Message"] = "Your Browse page.";
            // Retrieve Genre and its Associated Albums from database
            var categorieModel = from a in storeDB.Products where a.CategorieId == categorieId select a;
            if ((categorieId == 0)){
                return View("fakeError",storeDB.Products);
            }
            else {
            if (!String.IsNullOrEmpty(searchString))
            {
                categorieModel = storeDB.Products.Where(s => s.Title.Contains(searchString));
            }
            else
            {
                categorieModel = from a in storeDB.Products where a.CategorieId == categorieId select a;
            }
            return View(categorieModel);
            }
        }
        
        public IActionResult fakeError()
        {
            ViewData["Message"] = "Your 404 page.";
            return Content("Hi there!");
        }
        public IActionResult Item(int productId)
        {
            ViewData["Message"] = "Your Item page.";
            // Retrieve Genre and its Associated Albums from database
            var productModel = from a in storeDB.Products where a.Id == productId select a;

            return View(productModel);
        }
        public IActionResult Categorie()
        {
            ViewData["Message"] = "Your categorie page.";
            var categories = storeDB.Categories.ToList();
            return View(categories);
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
