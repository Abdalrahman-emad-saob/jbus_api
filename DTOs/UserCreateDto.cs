using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserCreateDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required]
        public string? Email { get; set; }
        public string? Sex { get; set; }
    }
}