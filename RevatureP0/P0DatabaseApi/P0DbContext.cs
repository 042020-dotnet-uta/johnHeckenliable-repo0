﻿using System;
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

        /*
        public void CreateSomeData()
        {
            
            CreateSomeStores();
            CreateSomeCustomers();
            CreateSomeProducts();
            CreateSomeStoreInventorys();
            CreateSomeOrderes();
            CreateSomeOrderDetails();
            
        }

        public void QuerySomeData()
        {
            ListCustomers();
            Console.WriteLine("***************************");
            ListStores();
            Console.WriteLine("***************************");
            ListProducts();
            Console.WriteLine("***************************");
            ListInventories();
            Console.WriteLine("***************************");
            ListStoresAndInventories();
            Console.WriteLine("***************************");
            ListOrders();
            Console.WriteLine("***************************");
            ListOrdersWithDetails();
        }

        public void UpdatePhoneNumber(string fName, string lName, string newPNumber)
        {
            var customer = this.Customers.FirstOrDefault(c => c.FirstName == fName && c.LastName == lName);

            Console.WriteLine($"Before Change - {customer.FirstName} {customer.LastName} phone number is {customer.PhoneNumber}");

            customer.PhoneNumber = newPNumber;
            this.SaveChanges();

            var customer2 = this.Customers.FirstOrDefault(c => c.FirstName == fName && c.LastName == lName);

            Console.WriteLine($"After Change - {customer2.FirstName} {customer2.LastName} phone number is {customer2.PhoneNumber}");
        }
        public void UpdateProductQuantity(int storeId, int productId, int quantityChange)
        {
            var store = this.Stores.FirstOrDefault(s => s.StoreId == storeId);
            var inventory = store.AvailableProducts.Where(s => s.ProductId == productId).FirstOrDefault();

            Console.WriteLine($"Before Change - {store.Location} has {inventory.Quantity} of product {productId}");

            inventory.Quantity += quantityChange;
            this.SaveChanges();

            var store2 = this.Stores.FirstOrDefault(s => s.StoreId == storeId);
            var inventory2 = store.AvailableProducts.FirstOrDefault(i => i.ProductId == productId);

            Console.WriteLine($"Before Change - {store2.Location} has {inventory2.Quantity} of product {productId}");
        }

        public void CreateOrder(int storeId, int custId, Dictionary<int, int> productsQuantity)
        {
            var order = new Order()
            {
                CusomerId = custId,
                StoreId = storeId,
                OrderDateTime = DateTime.Now
            };
            foreach (var product in productsQuantity)
            {
                var details = new OrderDetails()
                {
                    ProductId = product.Key,
                    Quantity = product.Value
                };
                order.ProductsOrdered.Add(details);
                UpdateProductQuantity(storeId, product.Key, product.Value * -1);
            }
            this.Add(order);
            this.SaveChanges();
        }

        #region Test data creation
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

        private void CreateSomeStoreInventorys()
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

        private void CreateSomeOrderes()
        {
            var order = new Order()
            {
                CusomerId = 1,
                StoreId = 1,
                OrderDateTime = DateTime.Now
            };
            this.Add(order);
            order = new Order()
            {
                CusomerId = 1,
                StoreId = 2,
                OrderDateTime = DateTime.Now
            };
            this.Add(order);
            order = new Order()
            {
                CusomerId = 1,
                StoreId = 3,
                OrderDateTime = DateTime.Now
            };
            this.Add(order);
            this.SaveChanges();
        }

        private void CreateSomeOrderDetails()
        {
            var orderDetail = new OrderDetails()
            {
                OrderId = 1,
                PricePaid = 9.99,
                ProductId = 1,
                Quantity = 5
            };
            this.Add(orderDetail);
            orderDetail = new OrderDetails()
            {
                OrderId = 1,
                PricePaid = 19.99,
                ProductId = 2,
                Quantity = 3
            };
            this.Add(orderDetail);
            orderDetail = new OrderDetails()
            {
                OrderId = 1,
                PricePaid = 15.99,
                ProductId = 3,
                Quantity = 2
            };
            this.Add(orderDetail);
            this.SaveChanges();
        }
        #endregion

        #region Test data queries
        private void ListCustomers()
        {
            var users = this.Customers.AsNoTracking();
            foreach (var customer in users)
            {
                Console.WriteLine($"Customer {customer.FirstName} {customer.LastName} has ID {customer.CustomerId}");
            }
        }
        private void ListStores()
        {
            var stores = this.Stores.AsNoTracking();
            foreach (var store in stores)
            {
                Console.WriteLine($"Store number {store.StoreId} is located in {store.Location}.");
            }
        }
        private void ListProducts()
        {
            var products = this.Products.AsNoTracking();
            foreach (var product in products)
            {
                Console.WriteLine($"Product {product.ProductDescription} costs {product.Price}");
            }
        }

        private void ListInventories()
        {
            var inventories = this.StoreInventories.AsNoTracking();
            foreach (var inventory in inventories)
            {
                Console.WriteLine($"StoreID {inventory.StoreId} has {inventory.Quantity} of ProductID {inventory.ProductId}");
            }
        }

        private void ListStoresAndInventories()
        {
            var stores = this.Stores;
            foreach (var store in stores.Include(o => o.AvailableProducts))
            {
                if (store.AvailableProducts == null)
                {
                    Console.WriteLine($"Store number {store.StoreId} located in {store.Location} has no available products");
                }
                else
                {
                    Console.WriteLine($"Store number {store.StoreId} located in {store.Location} has the following items:");
                    //var productInvs = this.StoreInventories;
                    foreach (var productInv in store.AvailableProducts)
                    {
                        var product = this.Products.Find(productInv.ProductId);
                        Console.WriteLine($"{product.ProductDescription} - Quantity of {productInv.Quantity}");
                    }
                }
            }
        }

        private void ListOrders()
        {
            var orders = this.Orders.AsNoTracking();
            foreach (var order in orders)
            {
                Console.WriteLine($"Order number {order.OrderId} happened at {order.OrderDateTime.ToShortDateString()} ");
            }
        }

        private void ListOrdersWithDetails()
        {
            var orders = this.Orders.AsNoTracking();
            foreach (var order in orders.Include(o => o.ProductsOrdered))
            {
                var customer = this.Customers.Find(order.CusomerId);
                var store = this.Stores.Find(order.StoreId);

                if (order.ProductsOrdered == null)
                {
                    Console.WriteLine($"Order number {order.OrderId} has no details");
                }
                else
                {
                    Console.WriteLine($"Order number {order.OrderId} Customer: {customer.FirstName + " " + customer.LastName} Store Location: {store.Location} included the following line items:");
                    foreach (var item in order.ProductsOrdered)
                    {
                        var product = this.Products.Find(item.ProductId);
                        Console.WriteLine($"{product.ProductDescription} - Quantity of {item.Quantity}");
                    }
                }
            }
        }
        #endregion
        */
    }
}