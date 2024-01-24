using API.DTOs;

namespace API.Interfaces
{
    public interface IDriverTripRepository
    {
        IEnumerable<DriverTripDto> GetDriverTrips();
        DriverTripDto GetDriverTripById(int id);
        DriverTripDto CreateDriverTrip(DriverTripCreateDto driverTripCreateDto, int id);
        bool SaveChanges();
    }
}