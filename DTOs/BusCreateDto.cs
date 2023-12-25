using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class BusCreateDto
    {
        [Required]
        public string? BusNumber { get; set; }
        // [Required]
        public int RouteId { get; set; }
        // [Required]
        public int DriverId { get; set; }
    }
}