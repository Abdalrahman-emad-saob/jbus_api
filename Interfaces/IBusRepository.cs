using API.DTOs;

namespace API.Interfaces
{
    public interface IBusRepository
    {
        void Update(BusDto busDto);
        IEnumerable<BusDto> GetBuses();
        BusDto GetBusById(int id);
        bool CreateBus(BusCreateDto busCreateDto);
        bool SaveChanges();
    }
}