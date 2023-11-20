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
        public string PasswordHash { get; set; }
        // public byte[] PasswordSalt { get; set; }
        public Gender UserGender { get; set; }
        public enum Gender
        {
            MALE = 0,
            FEMALE = 1
        }
        public DateOnly DateOfBirth { get; set; }

        // * Link
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        // * Methods
        // public int GetAge()
        // {
        //     var age = DateOnly.FromDateTime(DateTime.UtcNow).Year - DateOfBirth.Year;
        //     if (DateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-age))
        //         --age;
                
        //     return age;
        // }
    }
}