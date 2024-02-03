using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IScratchCardRepository
    {
        Task<IEnumerable<ScratchCardDto?>> GetScratchCards();
        string[] GetScratchCardsStatuses();
        Task<bool> CreateCard(ScratchCardCreateDto scratchCardCreateDto, int number);
        Task<ScratchCardDto?> GetScratchCardById(int id);
        Task<ScratchCardDto?> GetScratchCardByCN(int CardNumber);
        Task<ScratchCardDto?> ChargeCard(int CN, int passengerId);
        Task UpdateExpiredCards();
        Task<bool> SaveChanges();
    }
}