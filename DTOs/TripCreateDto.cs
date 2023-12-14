namespace API.DTOs
{
    public class TripCreateDto
    {
        // TODO TIMESTAMPS
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime FinishedAt { get; set; } = DateTime.UtcNow;
        public int Rating { get; set; }
        public string? status { get; set; }
        public enum Status
        {
            PENDING = 0,
            ONGOING = 1,
            COMPLETED = 2,
            CANCELED = 3
        }
        
        // * Link
        public int PassengerId { get; set; }
        public int BusId { get; set; }
        public int PaymentTransactionId { get; set; }
        public int RouteId { get; set; }
        public int PickUpPointId { get; set; }
        public int DropOffPointId { get; set; }
    }
}