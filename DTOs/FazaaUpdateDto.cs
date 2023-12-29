namespace API.DTOs
{
    public class FazaaUpdateDto
    {
        public DateTime ReturnedAt { get; set; }
        public double Amount { get; set; }
        public bool Paid { get; set; }
    }
}