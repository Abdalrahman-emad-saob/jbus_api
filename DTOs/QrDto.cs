using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class QrDto
    {
        [Required]
        public string? base64String { get; set; }
    }
}