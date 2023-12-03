namespace API.DTOs
{
    public class PaymentTransactionDto
    {
        // public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; }

        // * Link
        public int PassengerId { get; set; }
        public PassengerDto Passenger { get; set; }       
        public int TripId { get; set; }
        public TripDto Trip { get; set; }
    }
}