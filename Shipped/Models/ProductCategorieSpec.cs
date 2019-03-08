using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace login2.Models
{
 
  //this is the typed representation of a Categorie in our project
  public class Categorie
  {
    public int Id { get; set; }
    public List<Kabel> Kabels { get; set;}
    public List<Drone> Drones { get; set;}
    public List<Spelcomputer> Spelcomputers { get; set;}
    public List<Horloge> Horloges { get; set;}
    public List<Fotocamera> Fotocameras { get; set;}
    public List<Schoen> Schoenen { get; set;}
    public List<Cart> Carts {get;set;}
    public List<Wishlist> Wishlists {get;set;}
    public List<OrderHistory> OrderHistory {get;set;}
  }
  public class Cart
    {
        public int Id { get; set; }
        public int Order_nummer { get; set; }
        public string User_Id { get; set; }
        public int Product_Id { get; set; }
        public string Merk { get; set; }
        public string Model_naam { get; set; }
        public string Beschrijving { get; set; }
        public int Aantal { get; set; }
        public int Prijs { get; set; }

    }
 public class Wishlist
    {
        public int Id { get; set; }
        public int Order_nummer { get; set; }
        public string User_Id { get; set; }
        public int Product_Id { get; set; }
        public string Merk { get; set; }
        public string Model_naam { get; set; }
        public string Beschrijving { get; set; }
        public int Aantal { get; set; }
        public int Prijs { get; set; }

    }
public class OrderHistory
    {
        public int Id { get; set; }
        public string Order_nummer { get; set; }
        public string User_Id { get; set; }
        public int Product_Id { get; set; }
        public string Merk { get; set; }
        public string Model_naam { get; set; }
        public string Beschrijving { get; set; }
        public int Aantal { get; set; }
        public int Prijs { get; set; }
        public string Status {get;set;}

    }

  //this is the typed representation of a Kabel in our project
  public class Kabel
  {
    public int Id { get; set; }
    public string Type { get; set; } 
    public string Naam { get; set; }
    public int Prijs { get; set; }
    public string Merk { get; set; }
    public int Lengte { get; set; }
    public int Aantal { get; set; }
    public string Afbeelding { get; set; }
    public int Aantal_gekocht { get; set; }
    public int CategorieId { get; set; }
    public Categorie Categorie { get; set; }
  }
  
  //this is the typed representation of a Drone in our project
  public class Drone
  {
    public int Id { get; set; }
    public string Type { get; set; } 
    public string Naam { get; set; }
    public int Prijs { get; set; }
    public string Merk { get; set; }
    public string Kleur { get; set; }
    public int Aantal { get; set; }
    public string Afbeelding { get; set; }
    public int Aantal_gekocht { get; set; }
    public int CategorieId { get; set; }
    public Categorie Categorie { get; set; }
  }
  
  //this is the typed representation of a Console in our project
  public class Spelcomputer
  {
    public int Id { get; set; }
    public string Type { get; set; } 
    public string Naam { get; set; }
    public int Prijs { get; set; }
    public string Merk { get; set; }
    public int Geheugen { get; set; }
    public int Aantal { get; set; }
    public string Afbeelding { get; set; }
    public int Aantal_gekocht { get; set; }
    public int CategorieId { get; set; }
    public Categorie Categorie { get; set; }
  }

//this is the typed representation of a Horloge in our project
  public class Horloge
  {
    public int Id { get; set; }
    public string Type { get; set; } 
    public string Naam { get; set; }
    public int Prijs { get; set; }
    public string Merk { get; set; }
    public string Kleur { get; set; }
    public int Aantal { get; set; }
    public string Afbeelding { get; set; }
    public int Aantal_gekocht { get; set; }
    public int CategorieId { get; set; }
    public Categorie Categorie { get; set; }
  }

  //this is the typed representation of a Fotocamera in our project

  public class Fotocamera
  {
    public int Id { get; set; }
    public string Type { get; set; } 
    public string Naam { get; set; }
    public int Prijs { get; set; }
    public string Merk { get; set; }
    public string Megapixels { get; set; }
    public int Aantal { get; set; }
    public string Afbeelding { get; set; }
    public int Aantal_gekocht { get; set; }
    public int CategorieId { get; set; }
    public Categorie Categorie { get; set; }

  }

//this is the typed representation of a Schoen in our project
  public class Schoen
  {
    public int Id { get; set; }
    public string Type { get; set; } 
    public string Naam { get; set; }
    public int Prijs { get; set; }
    public string Merk { get; set; }
    public string Kleur { get; set; }
    public int Aantal { get; set; }
    public string Afbeelding { get; set; }
    public int Aantal_gekocht { get; set; }
    public int CategorieId { get; set; }
    public Categorie Categorie { get; set; }
    public int Maat { get; set; }
  }

}