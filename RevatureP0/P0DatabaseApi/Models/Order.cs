using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P0DatabaseApi
{
    public class Order
    {
		#region Properties
		[Key]
		public int OrderId { get; set; }

		[ForeignKey("Customer")]
		public int CusomerId { get; set; }

		[ForeignKey("Store")]
		public int StoreId { get; set; }

		private DateTime orderDateTime;
		public DateTime OrderDateTime
		{
			get { return orderDateTime; }
            set { orderDateTime = value; }
        }

		private List<OrderDetails> orderDetails;
		public List<OrderDetails> OrderDetails
		{
			get { return orderDetails; }
			set { orderDetails = value; }
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