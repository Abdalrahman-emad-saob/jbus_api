using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginAdminResponseDto
    {
        [Required]
        public AdminDto? adminDto { get; set; }
        [Required]
        public string? Token { get; set; }        
    }
}