using API.Entities;

namespace API.DTOs
{
    public class PointCreateDto
    {
        public string? Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}