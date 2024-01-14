using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class FazaaUpdateDto
    {
        [Required]
        public DateTime ReturnedAt { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public bool Paid { get; set; }
    }
}