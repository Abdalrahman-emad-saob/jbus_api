namespace API.DTOs
{
    public class RouteDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Fee { get; set; }
        public string? WaypointsGoing { get; set; }
        public string? WaypointsReturning { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int StartingPointId { get; set; }
        public InterestPointDto? StartingPoint { get; set; }
        public int EndingPointId { get; set; }
        public InterestPointDto? EndingPoint { get; set; }
        // public List<PredefinedStopsDto>? PredefinedStops { get; set; }
    }
}