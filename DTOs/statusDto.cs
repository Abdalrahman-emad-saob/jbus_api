using API.Entities;

namespace API.DTOs
{
    public class statusDto
    {
        public ZingyStatus status { get; set; }
        public TripDto? trip { get; set; }
        public RouteDto? route { get; set; }
        public BusDto? Bus { get; set; }
    }
}