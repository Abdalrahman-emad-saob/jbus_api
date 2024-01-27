using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IDriverRepository
    {
        void Update(DriverUpdateDto driver);
        Task<IEnumerable<DriverDto>> GetDrivers();
        Task<DriverDto?> GetDriverDtoById(int id);
        Task<Driver?> GetDriverById(int id);
        Task<Driver?> GetDriverByEmail(string Email);
        Task<DriverDto> CreateDriver(RegisterDriverDto driverDto);
        Task<bool> SaveChanges();
    }
}