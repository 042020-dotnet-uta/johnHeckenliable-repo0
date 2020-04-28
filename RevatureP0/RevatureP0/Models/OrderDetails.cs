using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0.Models
{
    class OrderDetails
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double PricePaid { get; set; }
    }
}
