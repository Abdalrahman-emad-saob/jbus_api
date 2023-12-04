namespace API.Entities
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        // * Link
        public int PassengerId { get; set; }
        public Passenger? Passenger { get; set; }       
        public int TripId { get; set; }
        public Trip? Trip { get; set; }
    }
}