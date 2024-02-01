using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class ScratchCardCreateDto
    {
        [Required]
        public int CardNumber { get; set; }
        [JsonIgnore]
        public string? Status { get; set; }
        public string? Type { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }        
    }
}