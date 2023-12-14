namespace API.DTOs
{
    public class BusUpdateDto
    {
        public string? BusNumber { get; set; }
        public int Capacity { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // * Link
        public int RouteId { get; set; }
        // public int DriverId { get; set; }
    }
}