namespace API.DTOs
{
    public class FriendDto
    {
        public int Id { get; set; }
        public string? ProfileImage { get; set; }
        public UserDto? User { get; set; }
    }
}