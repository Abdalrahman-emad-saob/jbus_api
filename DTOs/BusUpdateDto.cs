namespace API.DTOs
{
    public class BusUpdateDto
    {
        // public int Id { get; set; }
        public string BusNumber { get; set; }
        public int Capacity { get; set; }
        public DateTime UpdatedAt { get; set; }

        // * Link
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public int DriverId { get; set; }
        public DriverDto Driver { get; set; }
    }
}