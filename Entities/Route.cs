namespace API.Entities
{
    public class Route
    {
        // TODO - Add Soft Delete
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? WaypointsGoing { get; set; }
        public string? WaypointsReturning { get; set; }
        public double Fee { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // * Link
        public List<FavoritePoint>? FavoritePoints { get; set; }
        public List<Bus>? Buses { get; set; }
        public List<DriverTrip>? DriverTrips { get; set; }
        public int? StartingPointId { get; set; }
        public InterestPoint? StartingPoint { get; set; }
        public int? EndingPointId { get; set; }
        public InterestPoint? EndingPoint { get; set; }
        public int PredefinedStopsId { get; set; }
        public PredefinedStops? PredefinedStops { get; set; }
    }
}