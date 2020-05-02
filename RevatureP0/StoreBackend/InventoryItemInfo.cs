using System;
using System.Collections.Generic;
using System.Text;

namespace StoreBackend_Api
{
    public class InventoryItemInfo
    {
        public int ProductId { get; set; }
        public string ProductDescription { get; set; }
        public int Quantity { get; set; }

        public InventoryItemInfo() { }
        public InventoryItemInfo(int prodId, string prodDesc, int quantity) 
        {
            this.ProductId = prodId;
            this.ProductDescription = prodDesc;
            this.Quantity = quantity;
        }
    }
}
