using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class PredefinedStopsCreateDto
    {
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public int? RouteId { get; set; }
        [Required]
        public List<PointDto>? points { get; set; }

    }
}