using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P0DatabaseApi
{
    public class OrderDetails
    {
        [Key]
        public int OrderId { get; set; }
        [Key]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double PricePaid { get; set; }
    }
}
