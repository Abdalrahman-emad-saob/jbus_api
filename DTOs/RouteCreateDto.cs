using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RouteCreateDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? WaypointsGoing { get; set; }
        [Required]
        public string? WaypointsReturning { get; set; }
        [Required]
        public InterestPointCreateDto? StartingPoint { get; set; }
        [Required]
        public InterestPointCreateDto? EndingPoint { get; set; }
        public List<PredefinedStopsDto>? PredefinedStops { get; set; }

    }
}