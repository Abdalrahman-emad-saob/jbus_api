using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class InterestPointUpdateDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Logo { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public int LocationId { get; set; }
    }
}