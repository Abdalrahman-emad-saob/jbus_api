namespace API.Entities
{
    public class Passenger
    {
        public int Id { get; set; }
        public string? ProfileImage { get; set; }
        public double Wallet { get; set; }
        public string? GoogleToken { get; set; }
        public string? FacebookToken { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // * Link
        public List<ChargingTransaction> ChargingTransactions { get; set; } = [];
        public List<FavoritePoint> FavoritePoints { get; set; } = [];
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<PaymentTransaction> PaymentTransactions { get; set; } = [];
        public List<OTP> OTPs { get; set; } = [];
        public List<Trip> Trips { get; set; } = [];
    }
}