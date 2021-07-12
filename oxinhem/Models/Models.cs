using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MyModels;

namespace oxinhem.Models
{
    public class MyAppdbContext : DbContext
    {
        public MyAppdbContext() : base("db") { }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer<MyAppdbContext>(null);
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<City> cities { get; set; }
        public DbSet<Area> areas { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<SquareMeter> squareMeters { get; set; }
        public DbSet<Contact> contacts { get; set; }
        public DbSet<Blog> blogs { get; set; }
        public DbSet<About> abouts { get; set; }
        public DbSet<Request> requests { get; set; }
        public DbSet<Privacy> privacies { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<Painting> Paintings { get; set; }
        public DbSet<Cleaning> Cleanings { get; set; }
        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<Teams> Teams { get; set; }
    }
}
