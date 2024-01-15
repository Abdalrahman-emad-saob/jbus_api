using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class TripCreateDto
    {
        [Required(ErrorMessage = "The StartedAt field is required.")]
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        [Required(ErrorMessage = "The Status field is required.")]
        public string? status { get; set; }
        public int PaymentTransactionId { get; set; }
        [Required(ErrorMessage = "The PickUpPointId field is required.")]
        public int PickUpPointId { get; set; }
        public int DropOffPointId { get; set; }
    }
}