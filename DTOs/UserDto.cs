using static API.Entities.User;

namespace API.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? UserRole { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string? UserSex { get; set; }
    }
}