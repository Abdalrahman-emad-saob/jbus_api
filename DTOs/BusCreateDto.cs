using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class BusCreateDto
    {
        [Required]
        public string? BusNumber { get; set; }
        // [Required]
        public int RouteId { get; set; }
        // [Required]
        public int DriverId { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}