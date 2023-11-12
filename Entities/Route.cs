using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WaypointsGoing { get; set; }
        public string WaypointsReturning { get; set; }

        // * Link
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public int FavoritePointId { get; set; }
        public FavoritePoint FavoritePoint { get; set; }
        public int BusId { get; set; }
        public Bus Bus { get; set; }
        // TODO
        // [NotMapped]
        // public int StartingPointId { get; set; }
        // [NotMapped]
        // public InterestPoint StartingPoint { get; set; }
        // [NotMapped]
        // public int EndingPointId { get; set; }
        // [NotMapped]
        // public InterestPoint EndingPoint { get; set; }
    }
}