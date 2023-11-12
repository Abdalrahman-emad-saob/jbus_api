namespace API.Entities
{
    public class ChargingTransaction
    {
        public int Id { get; set; }
        // TODO
        public enum PaymentMethod { get, set, }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; }

        // * Link
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }
    }
}