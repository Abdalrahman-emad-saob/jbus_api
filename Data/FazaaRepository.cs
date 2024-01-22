using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class FazaaRepository : IFazaaRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FazaaRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool StoreFazaas(FazaaCreateDto fazaaCreateDto, int CreditorId)
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
                _context.Fazaas.Add(newFazaa);
            return true;
        }

        public FazaaDto GetFazaaById(int id)
        {
            return _context
           .Fazaas
           .Where(dt => dt.Id == id)
           .ProjectTo<FazaaDto>(_mapper.ConfigurationProvider)
           .SingleOrDefault()!;
        }

        public IEnumerable<FazaaDto> GetFazaas(int InDebtId)
        {
            return _context
           .Fazaas
           .Where(dt => dt.InDebtId == InDebtId || dt.CreditorId == InDebtId)
           .ProjectTo<FazaaDto>(_mapper.ConfigurationProvider)
           .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(FazaaDto fazaa)
        {
            _context.Entry(fazaa).State = EntityState.Modified;
        }
    }
}