using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("OTPs")]
    public class OTP
    {
        public int Id { get; set; }
        public int Otp { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // * Link
        public int PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
        
    }
}