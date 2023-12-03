using API.DTOs;

namespace API.Interfaces
{
    public interface IDriverRepository
    {
        void Update(DriverDto driver);
        IEnumerable<DriverDto> GetDrivers();
        DriverDto GetDriverById(int id);
        bool SaveChanges();
    }
}