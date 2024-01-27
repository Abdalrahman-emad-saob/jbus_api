using API.DTOs;

namespace API.Interfaces
{
    public interface IPredefinedStopsRepository
    {
        Task<IEnumerable<PredefinedStopsDto?>> GetPredefinedStops();
        Task<PredefinedStopsDto?> GetPredefinedStopById(int id);
        Task<PredefinedStopsDto?> CreatePredefinedStops(PredefinedStopsCreateDto predefinedStopsCreateDto);
        Task<bool> SaveChanges();
    }
}