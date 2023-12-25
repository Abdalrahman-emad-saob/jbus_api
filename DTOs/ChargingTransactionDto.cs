namespace API.DTOs
{
    public class ChargingTransactionDto
    {
         public int Id { get; set; }
        // TODO Charging Methods
        public enum ChargingMethod
        {
            MASTERCARD = 0,
            VISA = 1
        }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; }

        // * Link
        public int PassengerId { get; set; }
        public PassengerDto? Passenger { get; set; }
    }
}