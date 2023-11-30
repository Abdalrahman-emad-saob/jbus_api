using API.DTOs;

namespace API.Interfaces
{
    public interface IRouteRepository
    {
        void Update(RouteDto route);
        IEnumerable<RouteDto> GetRoutes();
        RouteDto GetRouteById(int id);
        bool SaveChanges();
    }
}