using IPOTEKA.UA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IPOTEKA.UA.Repostory
{
    public class MyDbContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<ProductBank> ProductsBank { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}