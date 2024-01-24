using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class DriverTripCreateDto
    {
        // [JsonIgnore]
        // public DateTime StartedAt { get; set; }
        // public DateTime FinishedAt { get; set; }
        // [Required]
        // public string? status { get; set; }
        // [Required]
        // public int? BusId { get; set; }
        // [Required]
        // public int? RouteId { get; set; }
        // [Required]
        public List<TripDto>? Trips { get; set; }
    }
}