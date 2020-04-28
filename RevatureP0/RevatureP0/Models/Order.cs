using System;
using System.Collections.Generic;
using System.Text;

namespace RevatureP0
{
    internal class Order
    {
		#region Properties
		public int OrderId { get; set; }

		private int customerID;
		public int CustomerID
		{
			get { return customerID; }
			set { customerID = value; }
		}

		private int storeID;
		public int StoreID
		{
			get { return storeID; }
			set { storeID = value; }
		}

		private DateTime orderDateTime;
		public DateTime OrderDateTime
		{
			get { return orderDateTime; }
            set { orderDateTime = value; }
        }

		private Dictionary<Product, int> products;
		public Dictionary<Product, int> Products
		{
			get { return products; }
		}
        #endregion

        #region Constructors
		internal Order()
		{ }
        #endregion

        #region Methods

        #endregion
    }
}