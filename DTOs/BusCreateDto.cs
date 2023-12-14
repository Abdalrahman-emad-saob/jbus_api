namespace API.DTOs
{
    public class BusCreateDto
    {
        public string? BusNumber { get; set; }
        // * Link
        public int RouteId { get; set; }
        public int DriverId { get; set; }
    }
}