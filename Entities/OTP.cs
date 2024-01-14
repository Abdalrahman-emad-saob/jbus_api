using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("OTPs")]
    public class OTP
    {
        public int Id { get; set; }
        public int Otp { get; set; }
        public DateTime CreatedAt { get; set; }

        // * Link
        public string? PassengerEmail { get; set; }
        
    }
}