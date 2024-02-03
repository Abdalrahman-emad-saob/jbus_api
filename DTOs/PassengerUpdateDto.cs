using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class PassengerUpdateDto
    {
        public string? ProfileImage { get; set; }
        public UserUpdateDto? User { get; set; }
    }
}