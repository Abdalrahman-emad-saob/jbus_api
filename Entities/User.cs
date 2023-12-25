namespace API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Role UserRole { get; set; }
        public enum Role
        {
            SUPER_ADMIN = 0,
            ADMIN = 1,
            DRIVER = 2,
            PASSENGER = 3
        }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string? PasswordHash { get; set; }
        public Sex UserSex { get; set; }
        public enum Sex
        {
            MALE = 0,
            FEMALE = 1
        }
        public DateOnly DateOfBirth { get; set; }

        // * Link
        public Passenger? Passenger { get; set; }
        public Driver? Driver { get; set; }
    }
}