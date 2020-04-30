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

		public DateTime OrderDateTime { get; set; }

		public virtual List<OrderDetails> ProductsOrdered { get; set; }

		#endregion

		#region Constructors
		public Order()
		{ ProductsOrdered = new List<OrderDetails>(); }
        #endregion

        #region Methods

        #endregion
    }
}