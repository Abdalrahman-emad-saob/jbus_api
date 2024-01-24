namespace API.Entities
{
    public class Point
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedAt { get; set; }

        // * Link
        public virtual List<FavoritePoint>? FavoritePoint { get; set; }
        public int? InterestPointId { get; set; }
        public virtual InterestPoint? InterestPoint { get; set; }
        public virtual List<Trip>? TripPickup { get; set; }
        public virtual List<Trip>? TripDropoff { get; set; }

    }
}