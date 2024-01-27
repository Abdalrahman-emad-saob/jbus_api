using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class BusUpdateDto
    {
        public string? BusNumber { get; set; }
        public int Capacity { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
        public int RouteId { get; set; }
        public int DriverId { get; set; }
    }
}