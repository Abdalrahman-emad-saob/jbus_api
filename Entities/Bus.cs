
namespace API.Entities
{
    public class Bus
    {
        public int Id { get; set; }
        public string BusNumber { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // * Link
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public List<Trip> Trips { get; set; }        
        
        
    }
}