namespace API.DTOs
{
    public class DriverUpdateDto
    {
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // * LINK
        public int UserId { get; set; }
        public UserDto? User { get; set; }
        public int BusId { get; set; }
        public BusDto? Bus { get; set; }
    }
}