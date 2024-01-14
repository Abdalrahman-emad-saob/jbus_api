using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class DriverUpdateDto
    {
        [Required]
        public UserUpdateDto? User { get; set; }
        [Required]
        public int BusId { get; set; }
    }
}