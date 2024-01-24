namespace API.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? WaypointsGoing { get; set; }
        public string? WaypointsReturning { get; set; }
        public double Fee { get; set; }
        public ActiveStatus IsActive { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // * Link
        public virtual List<FavoritePoint>? FavoritePoints { get; set; }
        public virtual List<Bus>? Buses { get; set; }
        public virtual List<DriverTrip>? DriverTrips { get; set; }
        public int? StartingPointId { get; set; }
        public virtual InterestPoint? StartingPoint { get; set; }
        public int? EndingPointId { get; set; }
        public virtual InterestPoint? EndingPoint { get; set; }
        public int PredefinedStopsId { get; set; }
        public virtual PredefinedStops? PredefinedStops { get; set; }
    }
}