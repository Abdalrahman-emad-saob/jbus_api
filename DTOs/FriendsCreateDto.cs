using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class FriendsCreateDto
    {
        [Required]
        public int FriendId { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}