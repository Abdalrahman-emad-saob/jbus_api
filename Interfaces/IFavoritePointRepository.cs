using API.DTOs;

namespace API.Interfaces
{
    public interface IFavoritePointRepository
    {
        void Update(FavoritePointDto favoritePoint);
        IEnumerable<FavoritePointDto> GetFavoritePoints(int id);
        FavoritePointDto GetFavoritePointById(int id);
        bool DeleteFavoritePoint(int id, int Id);
        bool InsertFavoritePoint(FavoritePointCreateDto favoritePointCreateDto, int id);
        bool SaveChanges();
        IEnumerable<FavoritePointDto> GetRouteFavoritePointDtos(int PassengerId, int RouteId);
    }
}