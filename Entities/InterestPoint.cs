namespace API.Entities
{
    public class InterestPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // * Link
        public int StartingPointId { get; set; }
        public Route StartingPoint { get; set; }
        public int EndingPointId { get; set; }
        public Route EndingPoint { get; set; }
        public int LocationId { get; set; }
        public Point Location { get; set; }
        
        

    }
}