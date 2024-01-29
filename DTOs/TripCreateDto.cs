using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class TripCreateDto
    {
        [Required(ErrorMessage = "The PickUpPoint field is required.")]
        public PointCreateDto? PickUpPoint { get; set; }
        public PointCreateDto? DropOffPoint { get; set; }
        // [Required(ErrorMessage = "The BusId field is required.")]
        // public int BusId { get; set; }
    }
}