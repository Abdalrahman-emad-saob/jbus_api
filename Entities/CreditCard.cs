namespace API.Entities
{
    public class CreditCard
    {
        public int Id { get; set; }
        public string? CardType { get; set; }
        public long CardNumber { get; set; }
        public short CVC { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public long Balance { get; set; }
    }
}