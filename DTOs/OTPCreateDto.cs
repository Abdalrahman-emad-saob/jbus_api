using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class OTPCreateDto
    {
        [Required]
        public int PassengerId { get; set; }
        [Required]
        public int Otp { get; set; }
    }
}