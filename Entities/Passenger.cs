namespace API.Entities
{
    public class Passenger
    {
        public int Id { get; set; }
        public string? ProfileImage { get; set; }
        public double Wallet { get; set; }
        public int RewardPoints { get; set; }
        public string? GoogleToken { get; set; }
        public string? FacebookToken { get; set; }
        public string? FcmToken { get; set; }

        // * Link
        public virtual List<ChargingTransaction> ChargingTransactions { get; set; } = [];
        public virtual List<FavoritePoint> FavoritePoints { get; set; } = [];
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual List<PaymentTransaction> PaymentTransactions { get; set; } = [];
        public virtual List<Trip> Trips { get; set; } = [];
        public virtual List<Fazaa>? InDebts { get; set; } = [];
        public virtual List<Fazaa>? Creditors { get; set; } = [];
    }
}