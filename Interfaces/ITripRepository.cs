using API.Entities;

namespace API.Interfaces
{
    public interface ITripRepository
    {
        void Update(Trip trip);
        IEnumerable<Trip> GetTrips();
        Trip GetTripById(int id);
        bool SaveChanges();
    }
}