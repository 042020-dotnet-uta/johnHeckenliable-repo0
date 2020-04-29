using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P0DatabaseApi
{
    public class Inventory
    {
        [Key][ForeignKey("Store")]
        public int StoreId { get; set; }

        [Key][ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
