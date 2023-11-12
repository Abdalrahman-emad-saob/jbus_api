namespace API.Entities
{
    public class Passenger
    {
        public int Id { get; set; }
        public string ProfileImage { get; set; }
        public double Wallet { get; set; }

        // * Link
        public int UserId { get; set; }
        public User User { get; set; }
        public int PaymentTransactionId { get; set; }
        public PaymentTransaction PaymentTransaction { get; set; }
        // public int OtpId { get; set; }
        public ICollection<OTP> OTPs { get; set; }

    }
}