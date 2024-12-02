using System.ComponentModel.DataAnnotations;

namespace ECommerce_Website.Models
{
    public class Cart
    {
        [Key]
        public int cart_id { get; set; }
        public int prod_id { get; set; }
        public int cust_id { get; set; }
        public int product_quantity { get; set; }
        public int cart_status { get; set; }
    }
}
