namespace API.DTOs
{
    public class DriverDto
    {
        // public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // * LINK
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int BusId { get; set; }
        public BusDto Bus { get; set; }
    }
}