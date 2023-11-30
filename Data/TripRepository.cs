using API.Entities;
using API.Interfaces;

namespace API.Data
{
    public class TripRepository : ITripRepository
    {
        public Trip GetTripById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Trip> GetTrips()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Trip trip)
        {
            throw new NotImplementedException();
        }
    }
}