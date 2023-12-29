using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class DriverTripCreateDto
    {
        [Required]
        public DateTime StartedAt { get; set; }
        [Required]
        public DateTime FinishedAt { get; set; }
        // TODO Function For Rating
        public int Rating { get; set; }
        [Required]
        public string? status { get; set; }
        [Required]
        public int? DriverId { get; set; }
        [Required]
        public int? BusId { get; set; }
        [Required]
        public int? RouteId { get; set; }
        // [Required]
        public List<TripDto>? Trips { get; set; }
    }
}