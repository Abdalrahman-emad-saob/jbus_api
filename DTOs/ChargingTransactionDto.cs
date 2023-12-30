namespace API.DTOs
{
    public class ChargingTransactionDto
    {
         public int Id { get; set; }
        public string? chargingMethod { get; set;}
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public int PassengerId { get; set; }
        // public PassengerDto? Passenger { get; set; }
    }
}