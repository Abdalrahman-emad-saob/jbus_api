namespace API.Entities
{
    public class FavoritePoint
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // public DateTime UpdatedAt { get; set; }

        // * Link
        public int? PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
        public int? PointId { get; set; }
        public Point? Point { get; set; }       
        public int? RouteId { get; set; }
        public Route? Route { get; set; }
        
    }
}