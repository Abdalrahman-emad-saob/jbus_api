using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class TripCreateDto
    {
        [Required]
        public DateTime StartedAt { get; set; }
        [Required]
        public DateTime FinishedAt { get; set; }
        [Required]
        public string? status { get; set; }
        // [Required]
        // public int PassengerId { get; set; }
        [Required]
        public int PaymentTransactionId { get; set; }
        [Required]
        public int PickUpPointId { get; set; }
        [Required]
        public int DropOffPointId { get; set; }
    }
}