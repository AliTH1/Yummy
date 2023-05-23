using System.ComponentModel.DataAnnotations;

namespace Yummy.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public List<Product> Products { get; set; }
    }
}
