using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class InterestPointCreateDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Logo { get; set; }   
        [Required]     
        public string? PointName { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}