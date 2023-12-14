namespace API.DTOs
{
    public class TripDto
    {
        // public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public int Rating { get; set; }
        public enum Status
        {
            PENDING = 0,
            ONGOING = 1,
            COMPLETED = 2,
            CANCELED = 3
        }
        

        // * Link
        public int PassengerId { get; set; }
        public PassengerDto? Passenger { get; set; }
        public int BusId { get; set; }
        public BusDto? Bus { get; set; }
        public int PaymentTransactionId { get; set; }
        public PaymentTransactionDto? PaymentTransaction { get; set; }
        public int RouteId { get; set; }
        public Route? Route { get; set; }     
        // TODO     
        public int PickUpPointId { get; set; }
        public PointDto? PickUpPoint { get; set; }
        public int DropOffPointId { get; set; }
        public PointDto? DropOffPoint { get; set; }
    }
}