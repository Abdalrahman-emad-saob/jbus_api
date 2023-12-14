using System.ComponentModel.DataAnnotations;
using API.Validations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string? Name { get; set; }
        [Phone]
        [Required]
        public string? PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required]
        public string? Email { get; set; }
        // [ValidPassword]
        [Required]
        public string? Password { get; set; }
        [Required]
        public int OTP { get; set; }

    }
}