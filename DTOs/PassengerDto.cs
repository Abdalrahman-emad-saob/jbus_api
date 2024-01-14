namespace API.DTOs
{
    public class PassengerDto
    {
        public int Id { get; set; }
        public string? ProfileImage { get; set; }
        public double Wallet { get; set; }
        public UserDto? User { get; set; }
    }
}