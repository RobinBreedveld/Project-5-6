using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Webshop.Models
{
  public class ProductContext : DbContext
  {
    //this is actual entity object linked to the movies in our DB
    public DbSet<Product> Products { get; set; }
    //this is actual entity object linked to the actors in our DB
    public DbSet<Categorie> Categories { get; set; }

    //this method is run automatically by EF the first time we run the application
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
      //here we define the name of our database
      optionsBuilder.UseNpgsql("User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=WebshopDB;Pooling=true;");
    }
  }

  //this is the typed representation of a product in our project
  public class Product
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public int CategorieId { get; set; }

  }

  //this is the typed representation of an Categorie in our project
  public class Categorie
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; }

  }
}