using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Webshop.Models;

namespace Webshop
{
    public class Program
    {
        public static void Main(string[] args){
        {
//             using (var db = new ProductContext())
// {

//   Categorie c = new Categorie
//   {
//     Name = "Cables",
//     Products = new System.Collections.Generic.List<Product> {
//       new Product{Title = "USB Cable", CategorieId = 2, Image = "~/images/usbcable.jpg"},
//       new Product{Title = "Hdmi Cable", CategorieId = 2,Image = "~/images/hdmicable.jpg"}
//     }
//   };
//   db.Categories.Add(c);
//   db.SaveChanges();
// }

            BuildWebHost(args).Run();
        }
        } 

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

