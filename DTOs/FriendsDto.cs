namespace API.DTOs
{
    public class FriendsDto
    {
        public int Id { get; set; }
        public PassengerDto? Friend { get; set; }
        public PassengerDto? Passenger { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ConfirmedAt { get; set; }
    }
}