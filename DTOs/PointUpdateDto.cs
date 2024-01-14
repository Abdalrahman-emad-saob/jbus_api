using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PointUpdateDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

    }
}