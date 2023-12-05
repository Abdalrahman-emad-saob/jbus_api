using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class ChargingTransaction
    {
        public int Id { get; set; }
        // TODO Payment Methods
        public enum PaymentMethod
        {
            MASTERCARD = 0,
            VISA = 1
        }
        public PaymentMethod paymentMethod { get; set; }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        // * Link
        public int PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
    }
}