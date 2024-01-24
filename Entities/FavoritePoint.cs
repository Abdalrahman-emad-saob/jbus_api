namespace API.Entities
{
    public class FavoritePoint
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        // * Link
        public int? PassengerId { get; set; }
        public virtual Passenger? Passenger { get; set; }
        public int? PointId { get; set; }
        public virtual Point? Point { get; set; }       
        public int? RouteId { get; set; }
        public virtual Route? Route { get; set; }
        
    }
}