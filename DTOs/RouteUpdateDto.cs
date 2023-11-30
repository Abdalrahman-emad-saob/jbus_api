namespace API.DTOs
{
    public class RouteUpdateDto
    {
        public string Name { get; set; }
        public string WaypointsGoing { get; set; }
        public string WaypointsReturning { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public List<BusUpdateDto> Buses { get; set; }
        public int StartingPointId { get; set; }
        public InterestPointDto StartingPoint { get; set; }
        public int EndingPointId { get; set; }
        public InterestPointDto EndingPoint { get; set; } 
    }
}