using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    class Store
    {
        #region Properties
        private int storeID;
        public int StoreID
        {
            get { return storeID; }
            //set { storeID = value; }
        }
        private string location;
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private Dictionary<int, int> availableProducts;
        public Dictionary<int, int> AvailableProducts
        {
            get { return availableProducts; }
        }
        #endregion

        #region Constructors
        public Store()
        {
            //Create StoreID???
        }
        public Store(int storeID, string location)
        {
            this.storeID = storeID;
            this.location = location;

            //Get available products???
        }
        #endregion

        #region Methods
        public bool UpdateInventory(int productID, int quantity)
        {
            return false;
        }
        #endregion
    }
}
