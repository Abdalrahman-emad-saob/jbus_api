namespace API.DTOs
{
    public class AdminUpdateDto
    {
        public int Id { get; set; }
        public UserUpdateDto? User { get; set; }
    }
}