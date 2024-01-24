namespace API.Entities
{
    public class Friends
    {
        public int Id { get; set; }
        public bool Confirmed { get; set; }
        public int? FriendId { get; set; }
        public virtual Passenger? Friend { get; set; }
        public int? PassengerId { get; set; }
        public virtual Passenger? Passenger { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ConfirmedAt { get; set; }
    }
}