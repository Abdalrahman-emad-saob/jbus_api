
using API.DTOs;

namespace API.Entities
{
    public class Fazaa
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ReturnedAt { get; set; }
        public double Amount { get; set; }
        public bool Paid { get; set; }
        // * Link
        public int? InDebtId { get; set; }
        public virtual Passenger? InDebt { get; set; }
        public int? CreditorId { get; set; }
        public virtual Passenger? Creditor { get; set; }
    }
}