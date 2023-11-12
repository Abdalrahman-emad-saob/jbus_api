namespace API.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public int Rating { get; set; }
        public enum Status { get, set }

        // * Link
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public int BusId { get; set; }
        public Bus Bus { get; set; }
        // TODO
        // public int PickUpPointId { get; set; }
        // public Point PickUpPoint { get; set; }
        // public int DropOffPointId { get; set; }
        // public Point DropOffPoint { get; set; }
    }
}