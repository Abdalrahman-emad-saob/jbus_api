using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class ChargingTransactionCreateDto
    {
        [Required]
        public string? paymentMethod { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public long CardNumber { get; set; }
        [Required]
        public short CVC { get; set; }
        [Required]
        public DateOnly ExpirationDate { get; set; }
        [JsonIgnore]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}