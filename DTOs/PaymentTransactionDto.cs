namespace API.DTOs
{
    public class PaymentTransactionDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public int PassengerId { get; set; }
        public int TripId { get; set; }
    }
}