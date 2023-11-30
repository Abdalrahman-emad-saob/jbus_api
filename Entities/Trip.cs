using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trips")]
    public class Trip
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime FinishedAt { get; set; } = DateTime.UtcNow;
        public int Rating { get; set; }
        public enum Status
        {
            PENDING = 0,
            ONGOING = 1,
            COMPLETED = 2,
            CANCELED = 3
        }
        

        // * Link
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }
        public int BusId { get; set; }
        public Bus Bus { get; set; }
        public int PaymentTransactionId { get; set; }
        public PaymentTransaction PaymentTransaction { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }     
        // TODO     
        public int PickUpPointId { get; set; }
        public Point PickUpPoint { get; set; }
        public int DropOffPointId { get; set; }
        public Point DropOffPoint { get; set; }
    }
}