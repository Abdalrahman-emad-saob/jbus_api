using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class DriverCreateDto
    { // TODO Alter Driver Dtos cause of the new Table Relations Predefined Stops
      // TODO Alter Passenger Dtos cause of the new Table Fazaa
      // TODO Alter Route Dtos cause of the new Table Predefined Stops
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        [Required]
        public string? UserSex { get; set; }
    }
}