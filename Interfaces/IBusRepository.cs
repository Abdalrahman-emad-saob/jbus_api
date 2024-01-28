using API.DTOs;

namespace API.Interfaces
{
    public interface IBusRepository
    {
        Task<bool> Update(BusUpdateDto busUpdateDto, int id);
        Task<IEnumerable<BusDto>> GetBuses();
        Task<IEnumerable<BusDto>> GetActiveBuses();
        Task<IEnumerable<BusDto>> GetActiveBusesByRoute(int id);
        Task<BusDto?> GetBusById(int id);
        Task<bool> De_ActivateBus(int? id);
        Task<bool> CreateBus(BusCreateDto busCreateDto);
        Task<bool> IsBusActive(int? id);
        Task<bool> SaveChanges();
    }
}