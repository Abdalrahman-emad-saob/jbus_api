using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class TripUpdateDto
    {
        [JsonIgnore]
        public string? Status { get; set; }
        public int PaymentTransactionId { get; set; }
        public PointCreateDto? PickUpPoint { get; set; }
        public PointCreateDto? DropOffPoint { get; set; }
    }
}