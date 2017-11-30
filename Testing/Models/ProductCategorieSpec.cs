using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace login2.Models
{
 
  //this is the typed representation of a movie in our project
  public class Categorie
  {
    public int Id { get; set; }
    public List<Cable> Cables { get;set;}
    public List<Gadget> Gadgets { get;set;}
  }

  //this is the typed representation of an actor in our project
  public class Cable
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price {get;set;}
    public int Length {get;set;}
    public string Type {get;set;}
    public int CategorieId {get;set;}
    public Categorie Categorie {get;set;}
  }
  public class Gadget
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price {get;set;}
    public string Type {get;set;}
    public int CategorieId {get;set;}
    public Categorie Categorie {get;set;}
  }
}