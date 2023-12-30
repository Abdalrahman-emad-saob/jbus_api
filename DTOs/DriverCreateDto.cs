using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class DriverCreateDto
    { // TODO Alter Driver Dtos cause of the new Repo Relations Predefined Stops
      // TODO Alter Passenger Dtos cause of the Repo Table Fazaa
      // TODO Alter Route Dtos cause of the new Repo Predefined Stops
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? UserSex { get; set; }
    }
}