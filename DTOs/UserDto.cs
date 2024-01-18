using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public string? Role { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastActive { get; set; }
        public string? Sex { get; set; }
    }
}