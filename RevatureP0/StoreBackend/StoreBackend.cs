using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using P0DatabaseApi;

namespace StoreBackend_Api
{
    public class StoreBackend
    {
        #region Fields
        P0DbContext db;
        #endregion

        #region Constructors
        public StoreBackend()
        {
            db = new P0DbContext();
        }
        public StoreBackend(P0DbContext context)
        {
            db = context;
        }
        #endregion

        #region Public Methods
        public CustomerInfo AddNewCustomer(string fName, string lName, string email)
        {
            email = email.ToLower();
            var customer = new Customer(fName, lName, email);
            db.Add(customer);
            db.SaveChanges();

            return new CustomerInfo(customer.CustomerId, fName, lName, email);
        }
        public CustomerInfo GetCustomerInfo(string email)
        {
            //Find the user in the db
            var customer = (from cust in db.Customers
                        where cust.Email == email.ToLower()
                        select new CustomerInfo 
                        { 
                            CustomerId = cust.CustomerId,
                            FirstName = cust.FirstName,
                            LastName = cust.LastName,
                            Email = cust.Email
                        }).Take(1).FirstOrDefault();

            return customer;
        }

        public List<CustomerInfo> SearchCustomersByFirstName(string fName)
        {
            //Find the user in the db
            var customers = (from cust in db.Customers
                            where cust.FirstName.ToLower() == fName.ToLower()
                            select new CustomerInfo
                            {
                                CustomerId = cust.CustomerId,
                                FirstName = cust.FirstName,
                                LastName = cust.LastName,
                                Email = cust.Email
                            }).ToList();

            return customers;
        }

        public List<CustomerInfo> SearchCustomersByLastName(string lName)
        {
            //Find the user in the db
            var customers = (from cust in db.Customers
                             where cust.LastName.ToLower() == lName.ToLower()
                             select new CustomerInfo
                             {
                                 CustomerId = cust.CustomerId,
                                 FirstName = cust.FirstName,
                                 LastName = cust.LastName,
                                 Email = cust.Email
                             }).ToList();

            return customers;
        }

        public List<OrderInfo> GetCustomerOrderHistory(int custId)
        {
            var orders = (from order in db.Orders
                           join cust in db.Customers on order.CusomerId equals cust.CustomerId
                           join store in db.Stores on order.StoreId equals store.StoreId
                           where order.CusomerId == custId
                           select new OrderInfo
                           {
                               OrderId = order.OrderId,
                               CustomerId = order.CusomerId,
                               CustomerName = $"{cust.FirstName} {cust.LastName}",
                               OrderDate = order.OrderDateTime,
                               StoreId = order.StoreId,
                               StoreLocation = store.Location,
                               LineItems = (from item in db.OrderDetails
                                            join prod in db.Products on item.ProductId equals prod.PoductId
                                            where item.OrderId == order.OrderId
                                            select new OrderLineItem
                                            {
                                                ProductId = item.ProductId,
                                                ProductDescrition = prod.ProductDescription,
                                                Quantity = item.Quantity,
                                                PricePaid = item.PricePaid
                                            }).ToList()//GetOrderLineItems(order.OrderId)
        }).ToList();
            return orders;
        }

        public List<LocationInfo> GetAllLocations()
        {
            var locations = (from loc in db.Stores
                            select new LocationInfo
                            {
                                StoreId = loc.StoreId,
                                Location = loc.Location,
                                AvailableProducts = (from item in db.StoreInventories
                                                     join prod in db.Products on item.ProductId equals prod.PoductId
                                                     where item.StoreId == loc.StoreId
                                                     select new InventoryItemInfo
                                                     {
                                                         ProductId = item.ProductId,
                                                         Quantity = item.Quantity,
                                                         ProductDescription = prod.ProductDescription,
                                                         Price = prod.Price
                                                     }).ToList()//GetAvailableProducts(loc.StoreId)
                            }).ToList();

            return locations;
        }

        public List<OrderInfo> GetLocationOrderHistory(int storeId)
        {
            var orders = (from order in db.Orders
                          join cust in db.Customers on order.CusomerId equals cust.CustomerId
                          join store in db.Stores on order.StoreId equals store.StoreId
                          where order.StoreId == storeId
                          select new OrderInfo
                          {
                              OrderId = order.OrderId,
                              CustomerId = order.CusomerId,
                              CustomerName = $"{cust.FirstName} {cust.LastName}",
                              OrderDate = order.OrderDateTime,
                              StoreId = order.StoreId,
                              StoreLocation = store.Location,
                              LineItems = (from item in db.OrderDetails
                                           join prod in db.Products on item.ProductId equals prod.PoductId
                                           where item.OrderId == order.OrderId
                                           select new OrderLineItem
                                           {
                                               ProductId = item.ProductId,
                                               ProductDescrition = prod.ProductDescription,
                                               Quantity = item.Quantity,
                                               PricePaid = item.PricePaid
                                           }).ToList()//GetOrderLineItems(order.OrderId)
        }).ToList();
            return orders;
        }

        public OrderInfo GetOrderInfo(int orderId)
        {
            var orderInfo = (from order in db.Orders
                         join cust in db.Customers on order.CusomerId equals cust.CustomerId
                         join store in db.Stores on order.StoreId equals store.StoreId
                         where order.OrderId == orderId
                         select new OrderInfo
                         {
                             OrderId = order.OrderId,
                             CustomerId = order.CusomerId,
                             CustomerName = $"{cust.FirstName} {cust.LastName}",
                             OrderDate = order.OrderDateTime,
                             StoreId = order.StoreId,
                             StoreLocation = store.Location,
                             LineItems = (from item in db.OrderDetails
                                          join prod in db.Products on item.ProductId equals prod.PoductId
                                          where item.OrderId == order.OrderId
                                          select new OrderLineItem
                                          {
                                              ProductId = item.ProductId,
                                              ProductDescrition = prod.ProductDescription,
                                              Quantity = item.Quantity,
                                              PricePaid = item.PricePaid
                                          }).ToList()//GetOrderLineItems(order.OrderId)
        }).Take(1).FirstOrDefault();

            return orderInfo;
        }

        public OrderInfo PlaceNewOrder(int storeId, int custId, List<ProductQuantity> items)
        {
            var detailItems = new List<OrderDetails>();

            foreach (var item in items)
            {
                if (!(CheckForEnoughInventory(storeId, item.ProductId, item.Quantity)))
                    throw new ArgumentOutOfRangeException($"Not enough inventory for product with ID {item.ProductId}");

                var detail = CreateOrderDetailItem(item.ProductId, item.Quantity);
                detailItems.Add(detail);
            }
            var order = new Order
            {
                CusomerId = custId,
                StoreId = storeId,
                OrderDateTime = DateTime.Now,
                ProductsOrdered = detailItems
            };
            db.Add(order);
            foreach (var item in order.ProductsOrdered)
            {
                UpdateLocationQuantity(order.StoreId, item.ProductId, (item.Quantity * -1));
            }
            db.SaveChanges();

            return GetOrderInfo(order.OrderId);
        }
        #endregion

        #region Private Methods
        private OrderDetails CreateOrderDetailItem(int prodId, int quantity)
        {
            var item = new OrderDetails
            {
                ProductId = prodId,
                Quantity = quantity,
                PricePaid = (from prod in db.Products
                             where prod.PoductId == prodId
                             select prod.Price).FirstOrDefault()
            };
            return item;
        }
        private bool CheckForEnoughInventory(int storeId, int prodId, int quantity)
        {
            var inventory = (from inv in db.StoreInventories
                             where inv.StoreId == storeId && inv.ProductId == prodId
                             select inv).Take(1).FirstOrDefault();

            return inventory.Quantity >= quantity;
        }
        private void UpdateLocationQuantity(int storeId, int prodId, int quatitiyUpdate)
        {
            var inventory = (from inv in db.StoreInventories
                             where inv.StoreId == storeId && inv.ProductId == prodId
                             select inv).Take(1).FirstOrDefault();

            inventory.Quantity += quatitiyUpdate;
        }
        /*
        private List<OrderLineItem> ttGetOrderLineItems(int orderId)
        {
            var lineItems = (from item in db.OrderDetails
                             join prod in db.Products on item.ProductId equals prod.PoductId
                             where item.OrderId == orderId
                             select new OrderLineItem
                             {
                                 ProductId = item.ProductId,
                                 ProductDescrition = prod.ProductDescription,
                                 Quantity = item.Quantity,
                                 PricePaid = item.PricePaid
                             }).ToList();
            return lineItems;
        }
        private List<InventoryItemInfo> GetAvailableProducts(int storeId)
        {
            var items = (from item in db.StoreInventories
                         join prod in db.Products on item.ProductId equals prod.PoductId
                         where item.StoreId == storeId
                         select new InventoryItemInfo
                         {
                             ProductId = item.ProductId,
                             Quantity = item.Quantity,
                             ProductDescription = prod.ProductDescription
                         }).ToList();
            return items;
        }
        */
        #endregion
    }
    public struct ProductQuantity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}