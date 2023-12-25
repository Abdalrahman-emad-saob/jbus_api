
namespace API.Entities
{
    public class PredefinedStops
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // * Link
        public int? RouteId { get; set; }
        public Route? Route { get; set; }
        public List<Point>? points { get; set; }

    }
}