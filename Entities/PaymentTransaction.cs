namespace API.Entities
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        // public bool IsValid { get; set; } = false;

        // * Link
        public int? PassengerId { get; set; }
        public Passenger? Passenger { get; set; }       
        public int? TripId { get; set; }
        public Trip? Trip { get; set; }
        public int? RouteId { get; set; }
        public Route? Route { get; set; }
        public int? BusId { get; set; }
        public Bus? Bus { get; set; }
        public int? DriverId { get; set; }
        public Driver? Driver { get; set; }
    }
}