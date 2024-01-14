using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class TripUpdateDto
    {
        public DateTime FinishedAt { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]
        public int PassengerId { get; set; }
        public int PaymentTransactionId { get; set; }
        [Required]
        public PointDto? PickUpPoint { get; set; }
        public PointDto? DropOffPoint { get; set; }
    }
}