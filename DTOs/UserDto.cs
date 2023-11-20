using System;
using API.Entities;
using static API.Entities.User;

namespace API.DTOs
{
    public class UserDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Role UserRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastActive { get; set; }
        public Gender UserGender { get; set; }
    }
}