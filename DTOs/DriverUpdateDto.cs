namespace API.DTOs
{
    public class DriverUpdateDto
    {
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // * LINK
        public UserDto? User { get; set; }
        public int BusId { get; set; }
    }
}