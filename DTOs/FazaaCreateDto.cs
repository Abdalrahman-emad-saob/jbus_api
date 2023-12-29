namespace API.DTOs
{
    public class FazaaCreateDto
    {
        public DateTime CreatedAt { get; set; }
        public double Amount { get; set; }
        public bool Paid { get; set; } = false;
        public int InDebtId { get; set; }
        public int CreditorId { get; set; }

    }
}