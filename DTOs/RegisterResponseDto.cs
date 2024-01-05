using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class RegisterResponseDto
    {
        [Required]
        public User? user { get; set; }
        [Required]
        public PassengerDto? passengerDto { get; set; }
    }
}