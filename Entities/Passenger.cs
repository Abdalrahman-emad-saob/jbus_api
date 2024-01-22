namespace API.Entities
{
    public class Passenger
    {
        public int Id { get; set; }
        public string? ProfileImage { get; set; }
        public double Wallet { get; set; }
        public string? GoogleToken { get; set; }
        public string? FacebookToken { get; set; }
        public string? FcmToken { get; set; }

        // * Link
        public List<ChargingTransaction> ChargingTransactions { get; set; } = [];
        public List<FavoritePoint> FavoritePoints { get; set; } = [];
        public int? UserId { get; set; }
        public User? User { get; set; }
        public List<PaymentTransaction> PaymentTransactions { get; set; } = [];
        public List<Trip> Trips { get; set; } = [];
        public List<Fazaa>? InDebts { get; set; } = [];
        public List<Fazaa>? Creditors { get; set; } = [];
    }
}