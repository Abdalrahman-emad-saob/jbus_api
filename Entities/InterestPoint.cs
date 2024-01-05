namespace API.Entities
{
    public class InterestPoint
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // * Link
        public int? RouteStartId { get; set; }
        public Route? RouteStart { get; set; }
        public int? RouteEndId { get; set; }
        public Route? RouteEnd { get; set; }
        public int? LocationId { get; set; }
        public Point? Location { get; set; }
        
        

    }
}