using API.Entities;

namespace API.Interfaces
{
    public interface IDriverRepository
    {
        void Update(Driver driver);
        IEnumerable<Driver> GetDrivers();
        Driver GetDriverById(int id);
        bool SaveChanges();
    }
}