namespace API.DTOs
{
    public class RouteUpdateDto
    {
        public string? Name { get; set; }
        public string? WaypointsGoing { get; set; }
        public string? WaypointsReturning { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int StartingPointId { get; set; }
        public int EndingPointId { get; set; }
        
    }
}