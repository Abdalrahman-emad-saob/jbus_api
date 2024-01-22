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
        [Required]
        public int RouteId { get; set; }
        [Required]
        public int BusId { get; set; }
        [Required]
        public int DriverId { get; set; }
        [JsonIgnore]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}