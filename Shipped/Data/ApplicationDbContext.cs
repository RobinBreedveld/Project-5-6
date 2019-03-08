using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using login2.Models;

namespace login2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Kabel> Kabels { get; set; }
        //this is actual entity object linked to the actors in our DB
        public DbSet<Drone> Drones { get; set; }
        public DbSet<Spelcomputer> Spelcomputers { get; set; }
        public DbSet<Horloge> Horloges { get; set; }
        public DbSet<Fotocamera> Fotocameras { get; set; }
        public DbSet<Schoen> Schoenen { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<OrderHistory> OrderHistory { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<login2.Models.ApplicationUser> ApplicationUser { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}
