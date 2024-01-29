using API.DTOs;

namespace API.Interfaces
{
    public interface ITripRepository
    {
        Task Update(TripUpdateDto trip, int id);
        Task<IEnumerable<TripDto?>> GetTrips(int PassengerId);
        Task<TripDto?> GetTripById(int id, int PassengerId);
        Task<IEnumerable<TripDto?>> GetTripsById(int id);
        Task<TripDto?> CreateTrip(TripCreateDto tripDto, int PassengerId, int BusId);
        Task<TripDto?> finishTrip(int id);
        Task<bool> SaveChanges();
    }
}