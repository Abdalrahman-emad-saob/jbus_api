namespace API.Entities
{
    public class DriverTrip
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public int Rating { get; set; }
        public Status status { get; set; }
        // * Link
        public int? DriverId { get; set; }
        public Driver? Driver { get; set; }
        public int? BusId { get; set; }
        public Bus? Bus { get; set; }
        public int? RouteId { get; set; }
        public Route? Route { get; set; }
        public List<Trip>? Trips { get; set; }
    }
}