using API.Entities;

namespace API.Interfaces
{
    public interface IFavoritePointRepository
    {
        void Update(FavoritePoint favoritePoint);
        IEnumerable<FavoritePoint> GetFavoritePoints();
        FavoritePoint GetFavoritePointById(int id);
        bool SaveChanges();
    }
}