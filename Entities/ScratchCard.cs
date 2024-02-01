namespace API.Entities;

public class ScratchCard
{
    public int Id { get; set; }
    public int CardNumber { get; set; }
    public ScratchCardStatus Status { get; set; }
    public ScratchCardType Type { get; set; }
    public int Amount { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UsedAt { get; set; }
}