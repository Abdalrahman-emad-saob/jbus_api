namespace API.DTOs
{
    public class FazaaDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ReturnedAt { get; set; }
        public double Amount { get; set; }
        public bool Paid { get; set; }
        public PassengerDto? InDebt { get; set; }
        public PassengerDto? Creditor { get; set; }
    }
}