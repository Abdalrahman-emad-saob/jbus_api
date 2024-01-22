using API.DTOs;

namespace API.Interfaces
{
    public interface IBusRepository
    {
        bool Update(BusUpdateDto busUpdateDto, int id);
        IEnumerable<BusDto> GetBuses();
        IEnumerable<BusDto> GetActiveBuses();
        BusDto GetBusById(int id);
        bool CreateBus(BusCreateDto busCreateDto);
        bool SaveChanges();
    }
}