using Microsoft.EntityFrameworkCore;
using RevatureP1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevatureP1.Data
{
    public class P1Context : DbContext
    {
        public P1Context(DbContextOptions<P1Context> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Inventory> StoreInventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()
                .HasKey(c => new { c.ProductId, c.OrderId });

            modelBuilder.Entity<Inventory>()
                .HasKey(c => new { c.ProductId, c.StoreId });
        }
    }
}
