namespace API.Entities
{
    public class Point
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // * Link
        public int FavoritePointId { get; set; }
        public FavoritePoint FavoritePoint { get; set; }

        // TODO
        // public int StartingPointId { get; set; }
        // public Point StartingPoint { get; set; }
        // public int EndingPointId { get; set; }
        // public Point EndingPoint { get; set; }
        // public int LocationId { get; set; }
        // public Point Location { get; set; }

    }
}