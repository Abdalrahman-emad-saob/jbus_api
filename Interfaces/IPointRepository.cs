using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPointRepository
    {
        void Update(PointDto point);
        Task<IEnumerable<PointDto?>> GetPoints();
        Task<PointDto?> GetPointById(int id);
        Task<Point?> CreatePoint(PointCreateDto pointDto);
        Task<PointDto?> PointExists(double lat, double lon);
        Task<bool> SaveChanges();
    }
}