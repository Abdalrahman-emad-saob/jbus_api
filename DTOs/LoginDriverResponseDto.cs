using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDriverResponseDto
    {
        [Required]
        public DriverDto? driverDto { get; set; }
        [Required]
        public string? Token { get; set; }
    }
}