using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class ScratchCardCreateDto
    {
        [Required]
        public string? Type { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }      
    }
}