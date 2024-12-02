using System.ComponentModel.DataAnnotations;

namespace ECommerce_Website.Models
{
    public class Category
    {
        [Key]
        public int category_id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string category_name { get; set; }

        public List<Product> Product { get; set; }
    }
}
