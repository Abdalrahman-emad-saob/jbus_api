using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class BusUpdateDto
    {
        [Required]
        public string? BusNumber { get; set; }
        [Required]
        public int Capacity { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public int RouteId { get; set; }
        // public int DriverId { get; set; }
    }
}