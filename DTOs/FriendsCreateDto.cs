using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class FriendsCreateDto
    {
        [Required]
        public int FriendId { get; set; }
    }
}