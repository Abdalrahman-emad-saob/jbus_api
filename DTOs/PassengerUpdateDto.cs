using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class PassengerUpdateDto
    {
        [Required]
        public string? ProfileImage { get; set; }
        [Required]
        public UserUpdateDto? User { get; set; }
    }
}