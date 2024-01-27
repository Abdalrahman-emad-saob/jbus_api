using API.DTOs;

namespace API.Interfaces
{
    public interface IFavoritePointRepository
    {
        void Update(FavoritePointDto favoritePoint);
        Task<IEnumerable<FavoritePointDto?>> GetFavoritePoints(int id);
        Task<FavoritePointDto?> GetFavoritePointById(int id);
        Task<bool> DeleteFavoritePoint(int id, int Id);
        Task<bool> InsertFavoritePoint(FavoritePointCreateDto favoritePointCreateDto, int id);
        Task<bool> SaveChanges();
        Task<IEnumerable<FavoritePointDto?>> GetRouteFavoritePointDtos(int PassengerId, int RouteId);
    }
}