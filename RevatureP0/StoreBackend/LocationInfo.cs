using System;
using System.Collections.Generic;
using System.Text;

namespace StoreBackend_Api
{
    public class LocationInfo
    {
        public int StoreId { get; set; }

        public string Location { get; set; }

        public List<InventoryItemInfo> AvailableProducts { get; set; }
    }
}
