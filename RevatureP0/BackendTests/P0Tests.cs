using Microsoft.EntityFrameworkCore;
using P0DatabaseApi;
using StoreBackend_Api;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace P0Tests
{
    public class StoreBackend_Tests
    {
        [Fact]
        public void AddsCustomerToDb()
        {
            //Arrange
            var options = BuildInMemoryDb("AddsCustomer");
            string fName = "Bob", lName = "Dole", email = "bdole@email.com";
            CustomerInfo customerInfo = null;

            //Act
            using (var context = new P0DbContext(options))
            {
                var backend = new StoreBackend(context);
                customerInfo = backend.AddNewCustomer(fName, lName, email);
            }
            //Assert
            using (var context = new P0DbContext(options))
            {
                var customer = (from c in context.Customers
                               where c.FirstName == fName && c.LastName == lName
                               select c).Take(1).FirstOrDefault();

                Assert.Equal(customer.CustomerId, customerInfo.CustomerId);
                Assert.Equal(fName, customer.FirstName);
                Assert.Equal(lName, customer.LastName);
                Assert.Equal(email, customer.Email);
            }
        }

        [Fact]
        public void GetsCustomerByEmail()
        {
            //Arrange
            var options = BuildInMemoryDb("GetsCustomer");
            string fName = "Bob", lName = "Dole", email = "bdole@email.com";

            //Act
            using (var context = new P0DbContext(options))
            {
                var customer = new Customer
                {
                    FirstName = fName,
                    LastName = lName,
                    Email = email
                };
                context.Add(customer);
                context.SaveChanges();
            }
            //Assert
            using (var context = new P0DbContext(options))
            {
                var backend = new StoreBackend(context);
                var customerInfo = backend.GetCustomerInfo(email);

                Assert.Equal(fName, customerInfo.FirstName);
                Assert.Equal(lName, customerInfo.LastName);
                Assert.Equal(email, customerInfo.Email);
            }
        }

        [Fact]
        public void GetsAllLocations()
        {
            //Arrange
            var options = BuildInMemoryDb("GetsAllLocations");

            //Act
            using (var context = new P0DbContext(options))
            {
                var store = new Store
                {
                    Location = "Location1"
                };
                context.Add(store);
                store = new Store
                {
                    Location = "Location2"
                };
                context.Add(store);
                store = new Store
                {
                    Location = "Location3"
                };
                context.Add(store);
                context.SaveChanges();
            }
            //Assert
            using (var context = new P0DbContext(options))
            {
                var backend = new StoreBackend(context);
                var stores = backend.GetAllLocations();

                Assert.Equal(3, stores.Count);
            }
        }

        [Fact]
        public void GetsCorrectInventoryCount()
        {
            //Arrange
            var options = BuildInMemoryDb("GetsInventoryCount");

            //Act
            using (var context = new P0DbContext(options))
            {
                CreateTwoproducts(context);

                var store = new Store
                {
                    StoreId = 1,
                    Location = "Location1",
                    AvailableProducts = new List<Inventory>
                    {
                        new Inventory
                        {
                            ProductId = 1,
                            StoreId = 1,
                            Quantity = 10
                        },
                        new Inventory
                        {
                            ProductId = 2,
                            StoreId = 1,
                            Quantity = 50
                        }
                    }
                };
                context.Add(store);
                context.SaveChanges();
            }
            //Assert
            using (var context = new P0DbContext(options))
            {
                var backend = new StoreBackend(context);
                var items = backend.GetAllLocations().FirstOrDefault().AvailableProducts; 

                Assert.Equal(2, items.Count);
            }
        }

        [Fact]
        public void GetsAllOrdersForLocation()
        {
            //Arrange
            var options = BuildInMemoryDb("GetsLocationOrders");

            //Act
            using (var context = new P0DbContext(options))
            {
                CreateOneCustomer(context);
                CreateTwoproducts(context);

                var store = new Store
                {
                    StoreId = 1,
                    Location = "Location1"
                };
                context.Add(store);
                context.SaveChanges();

                var order = new Order
                {
                    CusomerId = 1,
                    OrderDateTime = DateTime.Now,
                    OrderId = 1,
                    StoreId = 1,
                };
                context.Add(order); 
                order = new Order
                {
                    CusomerId = 1,
                    OrderDateTime = DateTime.Now,
                    OrderId = 2,
                    StoreId = 1,
                };
                context.Add(order);
                context.SaveChanges();
            }
            //Assert
            using (var context = new P0DbContext(options))
            {
                var backend = new StoreBackend(context);
                var orders = backend.GetLocationOrderHistory(1);

                Assert.Equal(2, orders.Count);
            }
        }

        [Fact]
        public void GetsAllOrdersForCustomer()
        {
            //Arrange
            var options = BuildInMemoryDb("GetsCustomersOrders");

            //Act
            using (var context = new P0DbContext(options))
            {
                CreateOneCustomer(context);
                CreateTwoproducts(context);

                var store = new Store
                {
                    StoreId = 1,
                    Location = "Location1"
                };
                context.Add(store);
                context.SaveChanges();

                var order = new Order
                {
                    CusomerId = 1,
                    OrderDateTime = DateTime.Now,
                    OrderId = 1,
                    StoreId = 1,
                };
                context.Add(order);
                order = new Order
                {
                    CusomerId = 1,
                    OrderDateTime = DateTime.Now,
                    OrderId = 2,
                    StoreId = 1,
                };
                context.Add(order);
                context.SaveChanges();
            }
            //Assert
            using (var context = new P0DbContext(options))
            {
                var backend = new StoreBackend(context);
                var orders = backend.GetCustomerOrderHistory(1);

                Assert.Equal(2, orders.Count);
            }
        }

        [Fact]
        public void AddsOrderToDb()
        {
            //Arrange
            var options = BuildInMemoryDb("AddsOrderToDb");
            int orderId;

            //Act
            using (var context = new P0DbContext(options))
            {
                CreateOneCustomer(context);
                CreateTwoproducts(context);

                var store = new Store
                {
                    StoreId = 1,
                    Location = "Location1",
                    AvailableProducts = new List<Inventory>
                    {
                        new Inventory
                        {
                            ProductId = 1,
                            StoreId = 1,
                            Quantity = 10
                        },
                        new Inventory
                        {
                            ProductId = 2,
                            StoreId = 1,
                            Quantity = 50
                        }
                    }
                };
                context.Add(store);
                context.SaveChanges();

                var backend = new StoreBackend(context);
                var prods = new List<ProductQuantity>()
                {
                    new ProductQuantity()
                    {
                        ProductId = 1,
                        Quantity = 2
                    },
                    new ProductQuantity()
                    {
                        ProductId=  2,
                        Quantity=  5
                    }
                };
                
                orderId = backend.PlaceNewOrder(1, 1, prods).OrderId;
            }
            //Assert
            using (var context = new P0DbContext(options))
            {
                var orders = from ord in context.Orders
                              where ord.OrderId == orderId
                              select ord;

                Assert.Single(orders);
            }
        }

        [Fact]
        public void DecrementsInventoryOnOrder()
        {
            //Arrange
            var options = BuildInMemoryDb("DecrementsInventory");
            int orderId;

            //Act
            using (var context = new P0DbContext(options))
            {
                CreateOneCustomer(context);
                CreateTwoproducts(context);

                var store = new Store
                {
                    StoreId = 1,
                    Location = "Location1",
                    AvailableProducts = new List<Inventory>
                    {
                        new Inventory
                        {
                            ProductId = 1,
                            StoreId = 1,
                            Quantity = 10
                        },
                        new Inventory
                        {
                            ProductId = 2,
                            StoreId = 1,
                            Quantity = 50
                        }
                    }
                };
                context.Add(store);
                context.SaveChanges();

                var backend = new StoreBackend(context);
                var prods = new List<ProductQuantity>()
                {
                    new ProductQuantity()
                    {
                        ProductId = 1,
                        Quantity = 2
                    }
                };

                orderId = backend.PlaceNewOrder(1, 1, prods).OrderId;
            }
            //Assert
            using (var context = new P0DbContext(options))
            {
                var item = (from inv in context.StoreInventories
                            where inv.StoreId == 1 && inv.ProductId == 1
                            select inv).Take(1).FirstOrDefault();

                Assert.Equal(8, item.Quantity);
            }
        }

        [Fact]
        public void ThrowsOnNegativeInventory()
        {
            //Arrange
            var options = BuildInMemoryDb("ThrowsException");

            //Act
            using (var context = new P0DbContext(options))
            {
                CreateOneCustomer(context);
                CreateTwoproducts(context);

                var store = new Store
                {
                    StoreId = 1,
                    Location = "Location1",
                    AvailableProducts = new List<Inventory>
                    {
                        new Inventory
                        {
                            ProductId = 1,
                            StoreId = 1,
                            Quantity = 10
                        },
                        new Inventory
                        {
                            ProductId = 2,
                            StoreId = 1,
                            Quantity = 50
                        }
                    }
                };
                context.Add(store);
                context.SaveChanges();
            }
            //Assert
            using (var context = new P0DbContext(options))
            {
                var backend = new StoreBackend(context);
                var prods = new List<ProductQuantity>()
                {
                    new ProductQuantity()
                    {
                        ProductId = 1,
                        Quantity = 12
                    }
                };

                Assert.Throws<ArgumentOutOfRangeException>(() => backend.PlaceNewOrder(1, 1, prods));
            }
        }

        [Fact]
        public void CancelsOrderOnNegativeInventory()
        {
            //Arrange
            var options = BuildInMemoryDb("CancelsOrder");

            //Act
            using (var context = new P0DbContext(options))
            {
                CreateOneCustomer(context);
                CreateTwoproducts(context);

                var store = new Store
                {
                    StoreId = 1,
                    Location = "Location1",
                    AvailableProducts = new List<Inventory>
                    {
                        new Inventory
                        {
                            ProductId = 1,
                            StoreId = 1,
                            Quantity = 10
                        },
                        new Inventory
                        {
                            ProductId = 2,
                            StoreId = 1,
                            Quantity = 50
                        }
                    }
                };
                context.Add(store);
                context.SaveChanges();
                try
                {
                    var backend = new StoreBackend(context);
                    var prods = new List<ProductQuantity>()
                {
                    new ProductQuantity()
                    {
                        ProductId = 1,
                        Quantity = 12
                    }
                };

                    backend.PlaceNewOrder(1, 1, prods);
                }
                catch { }
            }
            //Assert
            using (var context = new P0DbContext(options))
            {
                var orders = (from o in context.Orders
                             select o).ToList();

                Assert.Empty(orders);
            }
        }

        private void CreateOneCustomer(P0DbContext context)
        {
            var customer = new Customer
            {
                CustomerId = 1,
                FirstName = "Jim",
                LastName = "Bob",
                Email = "jimmy@email.com"
            };
            context.Add(customer);
            context.SaveChanges();
        }
        private void CreateTwoproducts(P0DbContext context)
        {
            var product = new Product
            {
                PoductId = 1,
                Price = 2.99,
                ProductDescription = "Prod1"
            };
            context.Add(product);
            product = new Product
            {
                PoductId = 2,
                Price = 5.99,
                ProductDescription = "Prod2"
            };
            context.Add(product);
            context.SaveChanges();
        }

        private DbContextOptions<P0DbContext> BuildInMemoryDb(string name)
        {
            return new DbContextOptionsBuilder<P0DbContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;
        }
    }
}