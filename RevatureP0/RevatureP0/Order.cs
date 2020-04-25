using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    internal class Order
    {
        #region Properties
        private int orderID;
		public int OrderID
		{
			get { return orderID; }
			//set { orderID = value; }
		}

		private int customerID;
		public int CustomerID
		{
			get { return customerID; }
			//set { customerID = value; }
		}

		private int storeID;
		public int StoreID
		{
			get { return storeID; }
			//set { storeID = value; }
		}

		private DateTime orderDateTime;
		public DateTime OrderDateTime
		{
			get { return orderDateTime; }
            //set { orderDateTime = value; }
        }

		private Dictionary<Product, int> products;
		public Dictionary<Product, int> Products
		{
			get { return products; }
		}
        #endregion

        #region Constructors
		internal Order(int customerID, int storeID)
		{
			//generate a unique orderID

			this.customerID = customerID;
			this.storeID = storeID;
		}
        #endregion

        #region Methods
		public double GetTotalCost()
		{
			var totalCost = 0.0;

			return totalCost;
		}

		public bool AddProductToOrder(int productID, int quantity)
		{
			return false;
		}
        #endregion
    }
}