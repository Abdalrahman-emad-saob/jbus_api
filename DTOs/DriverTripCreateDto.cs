using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class DriverTripCreateDto
    {
      [Required]
         public string? IsGoing { get; set; }
    }
}