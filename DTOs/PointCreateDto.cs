using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class PointCreateDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}