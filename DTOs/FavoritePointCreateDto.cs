namespace API.DTOs
{
    public class FavoritePointCreateDto
    {
        // * Link
        public int PassengerId { get; set; }
        public string? Name { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public int RouteId { get; set; }
    }
}