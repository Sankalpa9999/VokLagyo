using System.ComponentModel.DataAnnotations;

namespace ECommerce_Website.Models
{
    public class Rating
    {
        [Key]
        public int rating_id { get; set; }

        // Foreign key to Product
        public int product_id { get; set; }  // Make sure this property is correctly mapped
        public Product Product { get; set; }

        // Foreign key to Customer
        public int customer_id { get; set; }
        public Customer Customer { get; set; }

        public int rating_value { get; set; } // Rating value (1 to 5 for example)
        public DateTime date { get; set; } // Date when the rating was given
    }
}
