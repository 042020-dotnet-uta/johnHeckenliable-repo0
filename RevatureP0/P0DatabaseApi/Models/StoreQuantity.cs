using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P0DatabaseApi
{
    public class StoreQuantity
    {
        [Key]
        public int StoreId { get; set; }
        [Key]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
