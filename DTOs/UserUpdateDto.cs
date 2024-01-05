using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class UserUpdateDto
    {
        public string? Name { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        // TODO TO REMEMBER [RegularExpression(@"^[A-Za-z0-9_-]*$")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string? Sex { get; set; }
    }
}