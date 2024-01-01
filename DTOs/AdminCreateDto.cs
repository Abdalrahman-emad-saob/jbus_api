using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AdminCreateDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Sex { get; set; }
    }
}