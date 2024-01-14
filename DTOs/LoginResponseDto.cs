using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginResponseDto
    {
        [Required]
        public PassengerDto? passengerDto { get; set; }
        [Required]
        public string? Token { get; set; }        
    }
}