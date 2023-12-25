using API.DTOs;

namespace API.Interfaces
{
    public interface IFavoritePointRepository
    {
        void Update(FavoritePointDto favoritePoint);
        IEnumerable<FavoritePointDto> GetFavoritePoints(int id);
        FavoritePointDto GetFavoritePointById(int id);
        bool DeleteFavoritePoint(int id);
        bool InsertFavoritePoint(FavoritePointCreateDto favoritePointCreateDto);
        bool SaveChanges();
        IEnumerable<FavoritePointDto> GetRouteFavoritePointDtos(int PassengerId, int RouteId);
    }
}