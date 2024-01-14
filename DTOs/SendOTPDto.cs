using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class sendOTPDto
    {
        [Required]
        public string? Email { get; set; }
    }
}