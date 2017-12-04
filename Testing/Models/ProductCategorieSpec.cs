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
  }

  //this is the typed representation of a Kabel in our project
  public class Kabel
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
    public int Lengte { get; set; }
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
    public int Aantal_rotors { get; set; }
    public int Grootte { get; set; }
  }
  
  //this is the typed representation of a Console in our project
  public class Spelcomputer
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
    public int opslagcapaciteit { get; set; }
    public string Opties { get; set; }
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
    public int Grootte { get; set; }
    public string Materiaal { get; set; }
    public string Geslacht { get; set; }
  }

  //this is the typed representation of a Fotocamera in our project

  public class Fotocamera
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
    public int MegaPixels { get; set; } 
    public string Flits { get; set; }
    public int Min_Bereik { get; set; }
    public int Max_Bereik { get; set; }

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
    public string Materiaal { get; set; }
    public string Geslacht { get; set; }
  }

}