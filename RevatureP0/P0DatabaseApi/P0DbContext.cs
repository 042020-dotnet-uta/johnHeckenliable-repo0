using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace P0DatabaseApi
{
    public class P0DbContext :DbContext
    {
        public P0DbContext()
        { }

        public P0DbContext(DbContextOptions<P0DbContext> options)
            : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Inventory> StoreInventories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //use this for SQLite
            if (!options.IsConfigured)
            { 
                options.UseLazyLoadingProxies()
                       .UseSqlite("Data Source=C:\\Revature_Repo\\p0_Data.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()
                .HasKey(c => new { c.ProductId, c.OrderId });

            modelBuilder.Entity<Inventory>()
                .HasKey(c => new { c.ProductId, c.StoreId });
        }
    }
}