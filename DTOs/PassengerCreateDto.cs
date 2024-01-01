namespace API.DTOs
{
    public class PassengerCreateDto
    {
        public string? ProfileImage { get; set; }
        public UserCreateDto? User { get; set; }
    }
}