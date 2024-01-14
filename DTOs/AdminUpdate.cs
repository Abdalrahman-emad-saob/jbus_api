using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AdminUpdateDto
    {
        [Required]
        public UserUpdateDto? User { get; set; }
    }
}