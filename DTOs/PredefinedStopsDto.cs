namespace API.DTOs
{
    public class PredefinedStopsDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int RouteId { get; set; }
        public List<PointDto> points { get; set; } = [];

    }
}