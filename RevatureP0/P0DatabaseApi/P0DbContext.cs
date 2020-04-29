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
        public DbSet<Inventory> StoreInventories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //use this for SQLite
            if (!options.IsConfigured)
                options.UseSqlite("Data Source=C:\\Revature_Repo\\p0_Data.db");

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

            modelBuilder.Entity<Inventory>()
                .HasKey(c => new { c.ProductId, c.StoreId });
        }

        public void CreateSomeData()
        {
            //CreateSomeStores();
           //CreateSomeCustomers();
            //CreateSomeProducts();

            CreateStoreInventorys();
        }
        private void CreateSomeStores()
        {
            var store = new Store()
            {
                Location = "Seattle"
            };
            this.Add(store);
            store = new Store()
            {
                Location = "Bellevue"
            };
            this.Add(store);
            store = new Store()
            {
                Location = "Renton"
            };
            this.Add(store);
            this.SaveChanges();
        }

        private void CreateSomeCustomers()
        {
            var customer = new Customer()
            {
                FirstName = "Fuck",
                LastName = "Off",
                PhoneNumber = "555-EAT-SHIT"
            };
            this.Add(customer);
            customer = new Customer()
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "555-123-4567"
            };
            this.Add(customer);
            customer = new Customer()
            {
                FirstName = "Jane",
                LastName = "Doe",
                PhoneNumber = "555-123-4567"
            };
            this.Add(customer);
            this.SaveChanges();
        }

        private void CreateSomeProducts()
        {
            var product = new Product()
            {
                ProductDescription = "Some Bullshit",
                Price = 19.99
            };
            this.Add(product);
            product = new Product()
            {
                ProductDescription = "Crappy Crap",
                Price = 29.99
            };
            this.Add(product);
            product = new Product()
            {
                ProductDescription = "Pure Awesomeness",
                Price = 9.99
            };
            this.Add(product);
            this.SaveChanges();
        }

        private void CreateStoreInventorys()
        {
            var storeInv = new Inventory()
            {
                Quantity = 10,
                ProductId = 1,
                StoreId = 1
            };
            this.Add(storeInv);
            storeInv = new Inventory()
            {
                Quantity = 10,
                ProductId = 2,
                StoreId = 1
            };
            this.Add(storeInv);
            storeInv = new Inventory()
            {
                Quantity = 10,
                ProductId = 3,
                StoreId = 1
            };
            this.Add(storeInv);
            this.SaveChanges();
        }
    }
}