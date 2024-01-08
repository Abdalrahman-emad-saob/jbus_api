namespace API.Entities
{
    public class Driver
    {
        public int Id { get; set; }
        // * LINK
        public int UserId { get; set; }
        public User? User { get; set; }
        public int? BusId { get; set; }
        public Bus? Bus { get; set; }
        public List<DriverTrip>? DriverTrips { get; set; }
    }
}