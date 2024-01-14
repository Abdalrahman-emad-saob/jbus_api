namespace API.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public TripStatus status { get; set; }
        

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