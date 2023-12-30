using API.DTOs;

namespace API.Interfaces
{
    public interface ITripRepository
    {
        void Update(TripDto trip);
        IEnumerable<TripDto> GetTrips(int PassengerId);
        TripDto GetTripById(int id);
        IEnumerable<TripDto> GetTripsById(int id);
        bool CreateTrip(TripCreateDto tripDto, int PassengerId);
        bool SaveChanges();
    }
}