using API.DTOs;

namespace API.Interfaces
{
    public interface ITripRepository
    {
        void Update(TripDto trip);
        IEnumerable<TripDto> GetTrips();
        TripDto GetTripById(int id);
        IEnumerable<TripDto> GetTripsById(int id);
        bool SaveChanges();
    }
}