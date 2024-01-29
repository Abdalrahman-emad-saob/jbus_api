using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class FazaaRepository(DataContext context, IMapper mapper) : IFazaaRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> StoreFazaas(FazaaCreateDto fazaaCreateDto, int CreditorId)
        {
                // var passenger =_context.Passengers.Find(fazaa.CreditorId);
                Fazaa newFazaa = new()
                {
                    CreatedAt = DateTime.UtcNow,
                    Paid = false,
                    Amount = fazaaCreateDto.Amount,
                    CreditorId = CreditorId,
                    InDebtId = fazaaCreateDto.InDebtId
                };
                await _context.Fazaas.AddAsync(newFazaa);
            return true;
        }

        public async Task<FazaaDto?> GetFazaaById(int id)
        {
            return await _context
           .Fazaas
           .Where(dt => dt.Id == id)
           .ProjectTo<FazaaDto>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync()!;
        }

        public async Task<FazaaDto?> GetFazaaByPassengerId(int id)
        {
            return await _context
           .Fazaas
           .Where(dt => dt.InDebtId == id && dt.Paid == false)
           .ProjectTo<FazaaDto>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<FazaaDto?>> GetFazaas(int InDebtId)
        {
            return await _context
           .Fazaas
           .Where(dt => dt.InDebtId == InDebtId || dt.CreditorId == InDebtId)
           .ProjectTo<FazaaDto>(_mapper.ConfigurationProvider)
           .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(FazaaDto fazaa)
        {
            _context.Entry(fazaa).State = EntityState.Modified;
        }
    }
}