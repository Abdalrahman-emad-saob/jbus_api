namespace API.DTOs
{
    public class FriendsDto
    {
        public int Id { get; set; }
        public FriendDto? Friend { get; set; }
        public FriendDto? Passenger { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ConfirmedAt { get; set; }
    }
}