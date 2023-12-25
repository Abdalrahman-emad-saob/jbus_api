
namespace API.Entities
{
    public class Fazaa
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ReturnedAt { get; set; }
        public double Amount { get; set; }
        public bool Paid { get; set; }
        // public enum Status {
        //     UnPaid = 0,
        //     Paid = 1
        // }

        // * Link
        public int InDebtId { get; set; }
        public Passenger? InDebt { get; set; }
        public int CreditorId { get; set; }
        public Passenger? Creditor { get; set; }
        
    }
}