namespace API.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WaypointsGoing { get; set; }
        public string WaypointsReturning { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // * Link
        // TODO
        // public int FavoritePointId { get; set; }
        // public FavoritePoint FavoritePoint { get; set; }
        // public int BusId { get; set; }
        // public ICollection<Bus> Buses { get; set; }
        // public int TripId { get; set; }
        // public ICollection<Trip> Trips { get; set; }
        // [NotMapped]
        // public int StartingPointId { get; set; }
        // [NotMapped]
        // public InterestPoint StartingPoint { get; set; }
        // [NotMapped]
        // public int EndingPointId { get; set; }
        // [NotMapped]
        // public InterestPoint EndingPoint { get; set; }
    }
}