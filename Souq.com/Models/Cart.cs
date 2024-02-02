using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Souq.com.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public float? Price { get; set; }
        public int? Quentity {  get; set; }
        [DisplayName("User Name")]
        public String? UserId { get; set; }
        [ForeignKey("product")]
        public int ProductId { get; set; }
        public Product? product { get; set; }
    }
}
