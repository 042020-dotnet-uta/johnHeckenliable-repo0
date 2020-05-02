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
        public CustomerInfo CreateNewCustomer(string fName, string lName, string phoneNum)
        {
            var customer = new Customer(fName, lName, phoneNum);
            db.Add(customer);
            db.SaveChanges();

            return new CustomerInfo(customer.CustomerId, fName, lName, phoneNum);
        }
        public CustomerInfo GetCustomer(string fName, string lName)
        {
            //Find the user in the db
            var customer = (from cust in db.Customers
                        where cust.FirstName == fName && cust.LastName == lName
                        select new CustomerInfo 
                        { 
                            CustomerId = cust.CustomerId,
                            FirstName = cust.FirstName,
                            LastName = cust.LastName,
                            PhoneNumber = cust.PhoneNumber
                        }).Take(1).FirstOrDefault();

            return customer;
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
                               LineItems = GetOrderLineItems(order.OrderId)
                           }).ToList();
            return orders;
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
                              LineItems = GetOrderLineItems(order.OrderId)
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
                             LineItems = GetOrderLineItems(order.OrderId)
                         }).Take(1).FirstOrDefault();

            return orderInfo;
        }
        public List<OrderLineItem> GetOrderLineItems(int orderId)
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

        public OrderInfo PlaceNewOrder(int storeId, int custId, int[,] items)
        {
            var detailItems = new List<OrderDetails>();
            for (int i = 0; i < items.GetLength(0); i++)
            {
                var detail = CreateOrderDetailItem(items[i, 0], items[i, 1]);
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
            db.SaveChanges();

            return GetOrderInfo(order.OrderId);
        }


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
        #endregion
    }
}