using API.DTOs;

namespace API.Interfaces
{
    public interface IScratchCardRepository
    {
        Task<IEnumerable<ScratchCardDto?>> GetScratchCards();
        Task<ScratchCardDto?> CreateCard(ScratchCardCreateDto scratchCardCreateDto);
        Task<ScratchCardDto?> GetScratchCardById(int id);
        Task<ScratchCardDto?> ChargeCard(int id, int passengerId);
        Task UpdateExpiredCards();
        Task<bool> SaveChanges();
    }
}