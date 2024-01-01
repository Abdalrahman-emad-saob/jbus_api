namespace API.DTOs
{
    public class LoginAdminResponseDto
    {
        public AdminDto? adminDto { get; set; }
        public string? Token { get; set; }        
    }
}