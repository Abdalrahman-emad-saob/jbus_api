using API.DTOs;

namespace API.Interfaces
{
    public interface IDriverTripRepository
    {
        IEnumerable<DriverTripDto> GetDriverTrips();
        DriverTripDto GetDriverTripById(int id);
        bool CreateDriverTrip(DriverTripCreateDto driverTripCreateDto);
        bool SaveChanges();
    }
}