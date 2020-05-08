using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevatureP1.Models
{
    public class Inventory
    {
        [Key]
        [ForeignKey("Store")]
        public int StoreId { get; set; }

        [Key]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Inventory() { }
    }
}