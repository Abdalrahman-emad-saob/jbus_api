namespace API.DTOs
{
    public class ChargingTransactionCreateDto
    {
        public string? paymentMethod { get; set; }
        public double Amount { get; set; }
        // * Link
        public int PassengerId { get; set; }
    }
}