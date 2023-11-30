using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Point
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // * Link
        public int FavoritePointId { get; set; }
        public FavoritePoint FavoritePoint { get; set; }
        public int InterestPointId { get; set; }
        public InterestPoint InterestPoint { get; set; }

        // TODO
        public int TripPickupId { get; set; }
        public Trip TripPickup { get; set; }
        public int TripDropoffId { get; set; }
        public Trip TripDropoff { get; set; }
        

    }
}