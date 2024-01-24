namespace API.Entities
{
    public class DriverTrip
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        // public DateTime UpdateAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public int Rating { get; set; }
        public Status status { get; set; }
        // * Link
        public int? DriverId { get; set; }
        public virtual Driver? Driver { get; set; }
        public int? BusId { get; set; }
        public virtual Bus? Bus { get; set; }
        public int? RouteId { get; set; }
        public virtual Route? Route { get; set; }
        public virtual List<Trip>? Trips { get; set; }
    }
}