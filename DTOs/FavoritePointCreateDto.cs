using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class FavoritePointCreateDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public double Lat { get; set; }
        [Required]
        public double Long { get; set; }
        [Required]
        public int RouteId { get; set; }
    }
}