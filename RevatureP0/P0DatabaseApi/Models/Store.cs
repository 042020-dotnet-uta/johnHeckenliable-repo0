using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P0DatabaseApi
{
    public class Store
    {
        #region Properties
        [Key]
        public int StoreId { get; set; }

        public string Location { get; set; }

        public virtual List<Inventory> AvailableProducts { get; set; }
        #endregion

        #region Constructors
        public Store()
        { AvailableProducts = new List<Inventory>(); }
        #endregion

        #region Methods
        #endregion
    }
}
