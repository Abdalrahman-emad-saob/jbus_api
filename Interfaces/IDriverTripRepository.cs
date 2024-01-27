using API.DTOs;

namespace API.Interfaces
{
    public interface IDriverTripRepository
    {
        Task<IEnumerable<DriverTripDto?>> GetDriverTrips();
        Task<DriverTripDto?> GetDriverTripById(int id);
        Task<(DriverTripDto, string)> CreateDriverTrip(int id);
        Task<(DriverTripDto, string)> updateDriverTrip(int id, DriverTripUpdateDto driverTripUpdateDto);
        Task<bool> SaveChanges();
    }
}