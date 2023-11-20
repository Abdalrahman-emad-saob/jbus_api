namespace API.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WaypointsGoing { get; set; }
        public string WaypointsReturning { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // * Link
        // public int FavoritePointId { get; set; }
        public List<FavoritePoint> FavoritePoints { get; set; }
        // public int BusId { get; set; }
        public List<Bus> Buses { get; set; }
        // public int TripId { get; set; }
        public List<Trip> Trips { get; set; }
        public int StartingPointId { get; set; }
        public InterestPoint StartingPoint { get; set; }
        public int EndingPointId { get; set; }
        public InterestPoint EndingPoint { get; set; } 
    }
}