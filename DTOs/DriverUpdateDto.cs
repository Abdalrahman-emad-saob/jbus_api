namespace API.DTOs
{
    public class DriverUpdateDto
    {
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public UserUpdateDto? User { get; set; }
        public int BusId { get; set; }
    }
}