using API.DTOs;

namespace API.Interfaces
{
    public interface IFazaaRepository
    {
        IEnumerable<FazaaDto> GetFazaas(int InDebtId);
        FazaaDto GetFazaaById(int id);
        bool StoreFazaas(FazaaCreateDto fazaaCreateDto, int InDebtId);
        void Update(FazaaDto fazaa);
        bool SaveChanges();
    }
}