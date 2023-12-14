namespace API.DTOs
{
    public class InterestPointCreateDto
    {
        public string? Name { get; set; }
        public string? Logo { get; set; }        
        // * Link
        public int LocationId { get; set; }
    }
}