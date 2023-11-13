using jbus_api.Entities;

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
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        // public int TripId { get; set; }
        public ICollection<Trip> Trips { get; set; }        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }
}