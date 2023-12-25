using System.ComponentModel.DataAnnotations;

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
    }
}