using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PassengerCreateDto
    {
        [Required]
        public string? ProfileImage { get; set; }
        [Required]
        public UserCreateDto? User { get; set; }
    }
}