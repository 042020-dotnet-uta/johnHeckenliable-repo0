using System;
using System.Collections.Generic;
using System.Text;

namespace StoreBackend_Api
{
    public class OrderInfo
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public string CustomerName { get; set; }
        public string StoreLocation { get; set; }

        public DateTime OrderDate { get; set; }
        public double OrderTotal 
        {
            get
            {
                var total = 0.0;
                foreach (var item in LineItems)
                {
                    total += (item.PricePaid * item.Quantity);
                }
                return total;
            }
            set { OrderTotal = value; }
        }

        public List<OrderLineItem> LineItems { get; set; }

    }
}
