using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IInterestPointRepository
    {
        void Update(InterestPointDto interestPoint);
        IEnumerable<InterestPointDto> GetInterestPoints();
        InterestPointDto GetInterestPointById(int id);
        InterestPoint CreateInterestPoint(InterestPointCreateDto interestPointDto);
        bool SaveChanges();
    }
}