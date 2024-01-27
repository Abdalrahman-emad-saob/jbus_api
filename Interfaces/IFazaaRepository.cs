using API.DTOs;

namespace API.Interfaces
{
    public interface IFazaaRepository
    {
        Task<IEnumerable<FazaaDto?>> GetFazaas(int InDebtId);
        Task<FazaaDto?> GetFazaaById(int id);
        Task<bool> StoreFazaas(FazaaCreateDto fazaaCreateDto, int InDebtId);
        void Update(FazaaDto fazaa);
        Task<bool> SaveChanges();
    }
}