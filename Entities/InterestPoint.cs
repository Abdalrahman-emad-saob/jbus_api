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
        public virtual Route? RouteStart { get; set; }
        public int? RouteEndId { get; set; }
        public virtual Route? RouteEnd { get; set; }
        public int? LocationId { get; set; }
        public virtual Point? Location { get; set; }
        
        

    }
}