using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API.Entities;

namespace API.DTOs
{
    public class RouteUpdateDto
    {
        public string? Name { get; set; }
        public double Fee { get; set; }
        public string? WaypointsGoing { get; set; }
        public string? WaypointsReturning { get; set; }
        [JsonIgnore]
        public ActiveStatus IsActive { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
        public InterestPointCreateDto? StartingPoint { get; set; }
        public InterestPointCreateDto? EndingPoint { get; set; }
        
    }
}