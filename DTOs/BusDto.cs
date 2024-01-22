using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class BusDto
    {
        public int Id { get; set; }
        public string? BusNumber { get; set; }
        public int Capacity { get; set; }
        public string? Going { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public int RouteId { get; set; }
        public int DriverId { get; set; }
    }
}