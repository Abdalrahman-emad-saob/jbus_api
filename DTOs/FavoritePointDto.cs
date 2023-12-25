namespace API.DTOs
{
    public class FavoritePointDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        // * Link
        public int PointId { get; set; }
        public PointDto? Point { get; set; }       
        public int RouteId { get; set; }
    }
}