using API.DTOs;
using API.Interfaces;

namespace API.Data
{
    public class PointRepository : IPointRepository
    {
        public PointDto GetPointById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PointDto> GetPoints()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(PointDto point)
        {
            throw new NotImplementedException();
        }
    }
}