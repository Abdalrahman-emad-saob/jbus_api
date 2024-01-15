using API.DTOs;

namespace API.Interfaces
{
    public interface ITripRepository
    {
        void Update(TripUpdateDto trip, int id);
        IEnumerable<TripDto> GetTrips(int PassengerId);
        TripDto GetTripById(int id, int PassengerId);
        IEnumerable<TripDto> GetTripsById(int id);
        TripDto CreateTrip(TripCreateDto tripDto, int PassengerId);
        bool SaveChanges();
    }
}