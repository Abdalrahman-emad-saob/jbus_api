
namespace API.Entities
{
    public class PredefinedStops
    {
        public int Id { get; set; }
        public ActiveStatus IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // * Link
        public int? RouteId { get; set; }
        public virtual Route? Route { get; set; }
        public virtual List<Point>? points { get; set; }

    }
}