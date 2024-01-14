using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class PaymentTransactionCreateDto
    {
        [Required]
        public double Amount { get; set; }
        [Required]
        public int PassengerId { get; set; }
        [Required]
        public int TripId { get; set; }
        [JsonIgnore]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}