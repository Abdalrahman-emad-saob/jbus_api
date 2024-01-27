using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IInterestPointRepository
    {
        void Update(InterestPointDto interestPoint);
        Task<IEnumerable<InterestPointDto?>> GetInterestPoints();
        Task<InterestPointDto?> GetInterestPointById(int id);
        Task<InterestPoint?> CreateInterestPoint(InterestPointCreateDto interestPointDto);
        Task<bool> SaveChanges();
    }
}