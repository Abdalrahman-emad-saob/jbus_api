namespace API.DTOs
{
    public class PointDto
    {
         public int Id { get; set; }
        public string? Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // * Link
        // public int FavoritePointId { get; set; }
        // public FavoritePoint FavoritePoint { get; set; }
        // public int InterestPointId { get; set; }
        // public InterestPoint InterestPoint { get; set; }

        // TODO
        // public int StartingPointId { get; set; }
        // public Trip StartingPoint { get; set; }
        // public int EndingPointId { get; set; }
        // public Trip EndingPoint { get; set; }
    }
}