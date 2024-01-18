using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class UserCreateDto
    {
        [Required]
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required]
        public string? Email { get; set; }
        public string? Sex { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
    }
}