using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce_Website.Models
{
    public class Order
    {
        [Key]
        public int order_id { get; set; }

        [Required]
        public int cart_id { get; set; }

        [Required]
        public int order_status { get; set; } // 0 for Pending, 1 for Completed, etc.

        // Navigation Property
        [ForeignKey("cart_id")]
        public Cart cart { get; set; }
    }
}
