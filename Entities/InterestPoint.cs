namespace API.Entities
{
    public class InterestPoint
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // * Link
        public int? RouteStartId { get; set; }
        public Route? RouteStart { get; set; }
        public int? RouteEndId { get; set; }
        public Route? RouteEnd { get; set; }
        public int? LocationId { get; set; }
        public Point? Location { get; set; }
        
        

    }
}