using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class RouteCreateDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public double Fee { get; set; }
        [Required]
        public string? WaypointsGoing { get; set; }
        public string? WaypointsReturning { get; set; }
        [Required]
        public InterestPointCreateDto? StartingPoint { get; set; }
        [Required]
        public InterestPointCreateDto? EndingPoint { get; set; }
        // public List<PredefinedStopsDto>? PredefinedStops { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}