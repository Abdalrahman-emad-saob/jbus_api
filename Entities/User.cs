namespace API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string GoogleToken { get; set; }
        public string FacebookToken { get; set; }
        public enum Role { get, set, }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        // * Link
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }
        public int BusId { get; set; }
        public Bus Bus { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public int FavoritePointId { get; set; }
        public FavoritePoint FavoritePoint { get; set; }
        public int ChargingTransactionId { get; set; }
        public ChargingTransaction ChargingTransaction { get; set; }

    }
}