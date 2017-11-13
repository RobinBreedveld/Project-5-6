using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace login2.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }



        public int CategorieId { get; set; }
        public Categorie Categorie {get;set;}
        public List<Spec> Spec { get; set; }
        
    }

    //this is the typed representation of an Categorie in our project

    public class Categorie
    {
        public int CategorieId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public List<Product> Products { get; set; }
        public int ProductId {get;set;}
        

    }
    public class Spec
    {
        public int SpecId { get; set; }
        public string Name { get; set; }
        public int Intvalue { get; set; }
        public string Stringvalue { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
