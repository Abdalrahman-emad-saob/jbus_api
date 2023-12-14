using static API.Entities.User;

namespace API.DTOs
{
    public class DriverCreateDto
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Role UserRole { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public Gender UserGender { get; set; }
    }
}