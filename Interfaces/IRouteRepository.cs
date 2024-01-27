using API.DTOs;

namespace API.Interfaces
{
    public interface IRouteRepository
    {
        Task<bool> Update(RouteUpdateDto routeUpdateDto, int id);
        Task<IEnumerable<RouteDto?>> GetRoutes();
        Task<RouteDto?> GetRouteById(int id);
        Task<bool> CreateRoute(RouteCreateDto routeDto);
        Task<bool> Delete(int id);
        Task<bool> SaveChanges();
    }
}