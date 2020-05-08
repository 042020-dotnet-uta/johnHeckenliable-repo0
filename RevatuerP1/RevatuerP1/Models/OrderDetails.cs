using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevatureP1.Models
{
    public class OrderDetails
    {
        [Key]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Key]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public double PricePaid { get; set; }

        public OrderDetails() { }
    }
}