namespace API.DTOs
{
    public class FavoritePointDto
    {
        // public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        // public DateTime UpdatedAt { get; set; }

        // * Link
        public int PassengerId { get; set; }
        public PassengerDto? Passenger { get; set; }
        public int PointId { get; set; }
        public PointDto? Point { get; set; }       
        public int RouteId { get; set; }
        public RouteDto? Route { get; set; }
    }
}