using API.DTOs;

namespace API.Interfaces
{
    public interface IFazaaRepository
    {
        IEnumerable<FazaaDto> GetFazaas();
        FazaaDto GetFazaaById(int id);
        bool CreateFazaa(FazaaCreateDto fazaaCreateDto);
        void Update(FazaaDto fazaa);
        bool SaveChanges();
    }
}