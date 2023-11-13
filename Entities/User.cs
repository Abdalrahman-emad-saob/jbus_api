namespace API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string GoogleToken { get; set; }
        public string FacebookToken { get; set; }
        
        // TODO enum
        public enum Role
        {
            SUPER_ADMIN = 0,
            ADMIN = 1,
            DRIVER = 2,
            PASSENGER = 3
        }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    // * Link
    public int PassengerId { get; set; }
    public Passenger Passenger { get; set; }
}
}