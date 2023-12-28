namespace API.DTOs
{
    public class PassengerUpdateDto
    {
        public string? ProfileImage { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public UserUpdateDto? User { get; set; }
    }
}