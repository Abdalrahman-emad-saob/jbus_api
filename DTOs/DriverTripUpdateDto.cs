using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class DriverTripUpdateDto
    {
        public DateTime FinishedAt { get; set; }
        // TODO Function For Rating
        public int? Rating { get; set; }
        public string? status { get; set; }
        // [Required]
        public List<TripDto>? Trips { get; set; }
    }
}