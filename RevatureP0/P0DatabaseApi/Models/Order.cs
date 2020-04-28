using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P0DatabaseApi
{
    public class Order
    {
		#region Properties
		[Key]
		public int OrderId { get; set; }

		public int CusomerId { get; set; }

		public int StoreId { get; set; }

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