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

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int? rating { get; set; } // Nullable in case the user hasn't rated yet

        // Navigation Property
        [ForeignKey("cart_id")]
        public Cart cart { get; set; }
    }
}
