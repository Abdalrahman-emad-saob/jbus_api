using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ChargingTransactionCreateDto
    {
        [Required]
        public string? paymentMethod { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public int PassengerId { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}