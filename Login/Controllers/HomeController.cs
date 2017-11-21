using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using login2.Data;
using login2.Models;
using System.IO;

namespace login2.Controllers
{


    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;

            var file = "data.csv";
            var sr = new StreamReader(file);
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var data = line.Split(new[] { ',' });
                    var categorie = new Categorie() { Name = data[1], Image = data[2] };
                    context.Categories.Add(categorie);
                }

                context.SaveChanges();





            

        
        }

        public async Task<IActionResult> Index()
        {
            var categorieenproduct = _context.Categories.Include(p => p.Products);
            return View(await categorieenproduct.ToListAsync());
        }

        public async Task<IActionResult> Browse(int categorieId, int Id, string searchString)
        {
            ViewData["Message"] = "Your Browse page.";

            // Retrieve Genre and its Associated Albums from database
            var categorieModel = from a in _context.Products.Include(p => p.Spec) where a.CategorieId == categorieId select a;


            if (!String.IsNullOrEmpty(searchString))
            {
                categorieModel = _context.Products.Include(p => p.Spec).Where(s => s.Name.Contains(searchString.ToUpper()));
            }
            else
            {
                categorieModel = from a in _context.Products.Include(p => p.Spec) where a.CategorieId == categorieId select a;
            }

            return View(await categorieModel.ToListAsync());

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
            var productModel = from a in _context.Products where a.ProductId == productId select a;

            return View(productModel);
        }
        public IActionResult Categorie()
        {
            ViewData["Message"] = "Your categorie page.";
            var categories = _context.Categories.ToList();
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
    }
}

