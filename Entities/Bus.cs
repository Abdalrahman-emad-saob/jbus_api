namespace API.Entities
{
    public class Bus
    {
        public int Id { get; set; }
        public string BusNumber { get; set; }
        public int Capacity { get; set; }

        // * Link
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}