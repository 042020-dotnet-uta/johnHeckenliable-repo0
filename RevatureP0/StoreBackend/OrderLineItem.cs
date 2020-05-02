using System;
using System.Collections.Generic;
using System.Text;

namespace StoreBackend_Api
{
    public class OrderLineItem
    {
        public int ProductId { get; set; }
        public string ProductDescrition { get; set; }

        public int Quantity { get; set; }
        public double PricePaid { get; set; }
    }
}
