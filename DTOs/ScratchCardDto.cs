namespace API.DTOs;

public class ScratchCardDto
{
    public int Id { get; set; }
    public int CardNumber { get; set; }
    public string? Status { get; set; }
    public string? Type { get; set; }
    public int Amount { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UsedAt { get; set; }
}