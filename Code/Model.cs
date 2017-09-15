using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Model
{
  public class UserContext : DbContext
  {
    //this is actual entity object linked to the users in our DB
    public DbSet<User> User { get; set; }

    //this method is run automatically by EF the first time we run the application
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
      //here we define the name of our database
      optionsBuilder.UseNpgsql("User ID=postgres;Password=rmb143BL;Host=localhost;Port=5432;Database=UserDB;Pooling=true;");
    }
  }

  //this is the typed representation of a user in our project
  public class User
  {
    public int Id { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
  }
}