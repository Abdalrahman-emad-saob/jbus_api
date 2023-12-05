using API.DTOs;

namespace API.Interfaces
{
    public interface IPointRepository
    {
        void Update(PointDto point);
        IEnumerable<PointDto> GetPoints();
        PointDto GetPointById(int id);
        bool CreatePoint(PointCreateDto pointDto);
        bool SaveChanges();
    }
}