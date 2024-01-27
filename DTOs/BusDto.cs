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
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public int? RouteId { get; set; }
        public RouteDto? Route { get; set; }
        public DriverDto? Driver { get; set; }
    }
}