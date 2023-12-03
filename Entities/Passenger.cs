namespace API.Entities
{
    public class Passenger
    {
        public int Id { get; set; }
        public string ProfileImage { get; set; }
        public double Wallet { get; set; }
        public string GoogleToken { get; set; }
        public string FacebookToken { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // * Link
        // public int ChargingTransactionId { get; set; }
        public List<ChargingTransaction> ChargingTransactions { get; set; } = new();
        // public int FavoritePointId { get; set; }
        public List<FavoritePoint> FavoritePoints { get; set; } = new();
        public int UserId { get; set; }
        public User User { get; set; }
        // public int PaymentTransactionId { get; set; }
        public List<PaymentTransaction> PaymentTransactions { get; set; } = new();
        // public int OtpId { get; set; }
        public List<OTP> OTPs { get; set; } = new();
        // public int TripId { get; set; }
        public List<Trip> Trips { get; set; } = new();
    }
}