using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class OTPCreateDto
    {
        [Required]
        public int PassengerId { get; set; }
        [Required]
        public int Otp { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}