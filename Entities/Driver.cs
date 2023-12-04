namespace API.Entities
{
    public class Driver
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // * LINK
        public int UserId { get; set; }
        public User? User { get; set; }
        public int BusId { get; set; }
        public Bus? Bus { get; set; }
    }
}