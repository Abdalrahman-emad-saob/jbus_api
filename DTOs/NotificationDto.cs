using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
public class NotificationDto
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Body { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }
    }
}