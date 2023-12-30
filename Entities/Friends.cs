namespace API.Entities
{
    public class Friends
    {
        public int Id { get; set; }
        public bool Confirmed { get; set; }
        public int? FriendId { get; set; }
        public Passenger? Friend { get; set; }
        public int? PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
    }
}