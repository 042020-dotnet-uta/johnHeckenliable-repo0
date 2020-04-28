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

        private string location;
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private List<Product> availableProducts;
        public List<Product> AvailableProducts
        {
            get { return availableProducts; }
        }
        #endregion

        #region Constructors
        public Store()
        { }
        #endregion

        #region Methods
        #endregion
    }
}
