namespace API.DTOs
{
    public class PaymentTransactionCreateDto
    {
        public double Amount { get; set; }

        // * Link
        public int PassengerId { get; set; }
        public int TripId { get; set; }
    }
}