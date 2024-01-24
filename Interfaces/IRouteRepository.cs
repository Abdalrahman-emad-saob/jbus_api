using API.DTOs;

namespace API.Interfaces
{
    public interface IRouteRepository
    {
        bool Update(RouteUpdateDto routeUpdateDto, int id);
        IEnumerable<RouteDto> GetRoutes();
        RouteDto GetRouteById(int id);
        bool CreateRoute(RouteCreateDto routeDto);
        bool Delete(int id);
        bool SaveChanges();
    }
}