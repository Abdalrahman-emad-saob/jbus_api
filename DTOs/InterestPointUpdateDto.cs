namespace API.DTOs
{
    public class InterestPointUpdateDto
    {
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int LocationId { get; set; }
    }
}