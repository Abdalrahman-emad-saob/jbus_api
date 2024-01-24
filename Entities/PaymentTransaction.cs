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
        public virtual Passenger? Passenger { get; set; }       
        public int? TripId { get; set; }
        public virtual Trip? Trip { get; set; }
        public int? RouteId { get; set; }
        public virtual Route? Route { get; set; }
        public int? BusId { get; set; }
        public virtual Bus? Bus { get; set; }
        public int? DriverId { get; set; }
        public virtual Driver? Driver { get; set; }
    }
}