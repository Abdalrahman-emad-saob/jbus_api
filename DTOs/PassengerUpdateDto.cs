namespace API.DTOs
{
    public class PassengerUpdateDto
    {
        public string? ProfileImage { get; set; }
        public double Wallet { get; set; }
        public UserDto? User { get; set; }
    }
}