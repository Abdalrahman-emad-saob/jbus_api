namespace API.Entities
{
    public class OTP
    {
        public int Id { get; set; }
        public int Otp { get; set; }

        // * Link
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }
    }
}