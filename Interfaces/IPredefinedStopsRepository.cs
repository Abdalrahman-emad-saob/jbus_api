using API.DTOs;

namespace API.Interfaces
{
    public interface IPredefinedStopsRepository
    {
        IEnumerable<PredefinedStopsDto> GetPredefinedStops();
        PredefinedStopsDto GetPredefinedStopById(int id);
        PredefinedStopsDto CreatePredefinedStops(PredefinedStopsCreateDto predefinedStopsCreateDto);
        bool SaveChanges();
    }
}