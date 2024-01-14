using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class RouteUpdateDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? WaypointsGoing { get; set; }
        [Required]
        public string? WaypointsReturning { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public int StartingPointId { get; set; }
        [Required]
        public int EndingPointId { get; set; }
        
    }
}