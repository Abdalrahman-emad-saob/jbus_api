namespace API.Entities
{
    public class DriverTrip
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime FinishedAt { get; set; } = DateTime.UtcNow;
        public int Rating { get; set; }
        public Status status { get; set; }
        public enum Status
        {
            PENDING = 0,
            ONGOING = 1,
            COMPLETED = 2,
            CANCELED = 3
        }


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