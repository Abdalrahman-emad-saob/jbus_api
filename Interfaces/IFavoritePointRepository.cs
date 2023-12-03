using API.DTOs;

namespace API.Interfaces
{
    public interface IFavoritePointRepository
    {
        void Update(FavoritePointDto favoritePoint);
        IEnumerable<FavoritePointDto> GetFavoritePoints();
        FavoritePointDto GetFavoritePointById(int id);
        bool SaveChanges();
    }
}