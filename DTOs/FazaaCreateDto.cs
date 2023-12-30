using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class FazaaCreateDto
    {
        [Required]
        public double Amount { get; set; }
        [Required]
        public int CreditorId { get; set; }
    }
}