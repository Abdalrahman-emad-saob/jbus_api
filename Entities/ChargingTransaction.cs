using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class ChargingTransaction
    {
        public int Id { get; set; }
        public ChargingMethod ChargingMethod { get; set; }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        // * Link
        public int? PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
    }
}