namespace API.DTOs
{
    public class RouteCreateDto
    {
        public string? Name { get; set; }
        public string? WaypointsGoing { get; set; }
        public string? WaypointsReturning { get; set; }

        // * Link
        public int StartingPointId { get; set; }
        public int EndingPointId { get; set; }
    }
}