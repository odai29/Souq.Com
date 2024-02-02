using System.ComponentModel;

namespace Souq.com.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        [DisplayName("User")]
        public string? UserId { get; set; }
    }
}
