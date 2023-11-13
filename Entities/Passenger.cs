namespace API.Entities
{
    public class Passenger
    {
        public int Id { get; set; }
        public string ProfileImage { get; set; }
        public double Wallet { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // * Link
        // public int ChargingTransactionId { get; set; }
        public ICollection<ChargingTransaction> ChargingTransactions { get; set; }
        // public int FavoritePointId { get; set; }
        public ICollection<FavoritePoint> FavoritePoints { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        // public int PaymentTransactionId { get; set; }
        public ICollection<PaymentTransaction> PaymentTransactions { get; set; }
        // public int OtpId { get; set; }
        public ICollection<OTP> OTPs { get; set; }
        // public int TripId { get; set; }
        public ICollection<Trip> Trips { get; set; }
    }
}