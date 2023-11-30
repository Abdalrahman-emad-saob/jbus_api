namespace API.DTOs
{
    public class PassengerDto
    {
        public int Id { get; set; }
        public string ProfileImage { get; set; }
        public double Wallet { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}