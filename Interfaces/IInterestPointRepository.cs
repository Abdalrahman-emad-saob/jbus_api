using API.DTOs;

namespace API.Interfaces
{
    public interface IInterestPointRepository
    {
        void Update(InterestPointDto interestPoint);
        IEnumerable<InterestPointDto> GetInterestPoints();
        InterestPointDto GetInterestPointById(int id);
        bool SaveChanges();
    }
}