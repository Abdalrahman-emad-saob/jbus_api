using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IDriverRepository
    {
        void Update(DriverDto driver);
        IEnumerable<DriverDto> GetDrivers();
        DriverDto GetDriverDtoById(int id);
        Driver GetDriverById(int id);
        bool CreateDrive(DriverCreateDto driverDto);
        bool SaveChanges();
    }
}