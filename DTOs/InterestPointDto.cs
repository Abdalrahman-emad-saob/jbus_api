namespace API.DTOs
{
    public class InterestPointDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int LocationId { get; set; }
        public PointDto? Location { get; set; }
    }
}