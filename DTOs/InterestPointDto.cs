using API.Entities;

namespace API.DTOs
{
    public class InterestPointDto
    {
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // * Link
        public int StartingPointId { get; set; }
        public RouteDto? StartingPoint { get; set; }
        public int EndingPointId { get; set; }
        public RouteDto? EndingPoint { get; set; }
        public int LocationId { get; set; }
        public PointDto? Location { get; set; }
    }
}