namespace API.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime FinishedAt { get; set; } = DateTime.UtcNow;
        public Status status { get; set; }
        public enum Status
        {
            PENDING = 0,
            ONGOING = 1,
            COMPLETED = 2,
            CANCELED = 3
        }
        

        // * Link
        public int? PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
        public int? PaymentTransactionId { get; set; }
        public PaymentTransaction? PaymentTransaction { get; set; }
        public int? DriverTripId { get; set; }
        public DriverTrip? DriverTrip { get; set; }     
        public int? PickUpPointId { get; set; }
        public Point? PickUpPoint { get; set; }
        public int? DropOffPointId { get; set; }
        public Point? DropOffPoint { get; set; }
    }
}