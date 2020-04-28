using System;
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
        public DbSet<StoreQuantity> StoreQuantities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //use this for SQLite
            if (!options.IsConfigured)
                options.UseSqlite("Data Source=p0_Data.db");

            //use this for localDb SSMS
            //if (!options.IsConfigured)
            //{
            //        options.UseSQLServer("Data Source = (LocalDB)\v11.0; AttachDbFileName =| DataDirectory |\DatabaseFileName.mdf; InitialCatalog = DatabaseName; Integrated Security = True; MultipleActiveResultSets = True");
            //}
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()
                .HasKey(c => new { c.ProductId, c.OrderId });

            modelBuilder.Entity<StoreQuantity>()
                .HasKey(c => new { c.ProductId, c.StoreId });
        }
    }
}
