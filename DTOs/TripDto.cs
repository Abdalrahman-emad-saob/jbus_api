namespace API.DTOs
{
    public class TripDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public enum Status
        {
            PENDING = 0,
            ONGOING = 1,
            COMPLETED = 2,
            CANCELED = 3
        }
        

        // * Link
        public int PassengerId { get; set; }
        public int PaymentTransactionId { get; set; }
        public int PickUpPointId { get; set; }
        public PointDto? PickUpPoint { get; set; }
        public int DropOffPointId { get; set; }
        public PointDto? DropOffPoint { get; set; }
    }
}