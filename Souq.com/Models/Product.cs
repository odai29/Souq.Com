using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Souq.com.Models
{
    public class Product
    {
        public int Id { get; set; }
        [DisplayName("Product Name")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public DateTime? Date { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile? ImageFile { get; set; }
        [ForeignKey("category")]
        public int CategoryId { get; set; }
        public Category? category { get; set; }
    }
}
