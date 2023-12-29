using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PredefinedStopsCreateDto
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public int? RouteId { get; set; }
        [Required]
        public List<PointDto>? points { get; set; }

    }
}