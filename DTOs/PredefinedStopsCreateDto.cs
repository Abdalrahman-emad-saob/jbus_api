using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class PredefinedStopsCreateDto
    {
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
        [Required]
        public int? RouteId { get; set; }
        [Required]
        public List<PointCreateDto>? points { get; set; }

    }
}