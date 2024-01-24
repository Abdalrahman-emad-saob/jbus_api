namespace API.Entities
{
    public class Driver
    {
        public int Id { get; set; }
        // * LINK
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public int? BusId { get; set; }
        public virtual Bus? Bus { get; set; }
        public virtual List<DriverTrip>? DriverTrips { get; set; }
    }
}