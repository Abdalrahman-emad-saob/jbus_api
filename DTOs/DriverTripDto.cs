namespace API.DTOs
{
    public class DriverTripDto
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public int Rating { get; set; }
        public string? status { get; set; }
        public int? DriverId { get; set; }
        public int? BusId { get; set; }
        public int? RouteId { get; set; }
        public List<TripDto>? Trips { get; set; }
    }
}