
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class EncryptedDataDto
    {
        [Required]
        public string? EncryptedData { get; set; }
    }
}