using System.ComponentModel.DataAnnotations;

namespace Yummy.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required, MaxLength(60)]
        public string Ingredients { get; set; }
        [Required]
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
