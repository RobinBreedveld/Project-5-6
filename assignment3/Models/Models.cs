using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;


namespace assignment3.Models
{
  public class MovieContext : DbContext
  {
    //this is actual entity object linked to the movies in our DB
    public DbSet<Movie> Movies { get; set; }
    //this is actual entity object linked to the actors in our DB
    public DbSet<Actor> Actors { get; set; }

    //Added constructor to provide the connection to the database as a service (look at: startup.cs)
    public MovieContext(DbContextOptions<MovieContext>options): base(options)
    {
        
    }
    //this method is run automatically by EF the first time we run the application
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
      //here we define the name of our database
    //  optionsBuilder.UseNpgsql("User ID=postgres;Password=kerimkaya;Host=localhost;Port=5432;Database=MovieDatabase;Pooling=true;");
    //}
    
}

  //this is the typed representation of a movie in our project
  public class Movie
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public List<Actor> Actors { get; set; }
  }

 

  //this is the typed representation of an actor in our project
  public class Actor
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int MovieId {get; set;}
  }
}
