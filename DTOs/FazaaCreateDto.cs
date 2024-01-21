using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class FazaaCreateDto
    {
        [Required]
        public double Amount { get; set; }
        [Required]
        public int InDebtId { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}