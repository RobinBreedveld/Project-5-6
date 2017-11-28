using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Testing.Models
{
  public class ModelContext : DbContext
  {
    //this is actual entity object linked to the movies in our DB
    public DbSet<Categorie> Categories { get; set; }
    public DbSet<Cable> Cables { get; set; }
    //this is actual entity object linked to the actors in our DB
    public DbSet<Gadget> Gadgets { get; set; }

    public ModelContext(DbContextOptions<ModelContext> options): base(options)
        {
        }

        //Uncomment or remove the OnConfiguring method. It is not need any more
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //
        //         // http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings
        //         optionsBuilder.UseNpgsql(@"Host=localhost;Database=MovieDB;Username=postgres;Password=postgres");
        //     }
        // }
  }

  //this is the typed representation of a movie in our project
  public class Categorie
  {
    public int Id { get; set; }
    public string Title { get; set; }
  }

  //this is the typed representation of an actor in our project
  public class Cable
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price {get;set;}
    public int Length {get;set;}
    public string Type {get;set;}
  }
  public class Gadget
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price {get;set;}
    public string Type {get;set;}
  }
}