using System;
using System.Collections.Generic;
using System.Text;

namespace P0DatabaseApi
{
    class OrderDetails
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double PricePaid { get; set; }
    }
}
