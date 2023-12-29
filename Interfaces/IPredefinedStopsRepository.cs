using API.DTOs;

namespace API.Interfaces
{
    public interface IPredefinedStopsRepository
    {
        IEnumerable<PredefinedStopsDto> GetPredefinedStops();
        PredefinedStopsDto GetPredefinedStopById(int id);
        bool CreatePredefinedStop(PredefinedStopsCreateDto predefinedStopsCreateDto);
        bool SaveChanges();
    }
}