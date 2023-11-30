namespace API.DTOs
{
    public class LoginResponseDto
    {
        public PassengerDto passengerDto { get; set; }
        public string Token { get; set; }        
    }
}