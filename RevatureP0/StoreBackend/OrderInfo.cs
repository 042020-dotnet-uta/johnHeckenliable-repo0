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
        public double OrderTotal { get; set; }

        public List<OrderLineItem> LineItems { get; set; }

    }
}
