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

        public bool CreateFazaa(FazaaCreateDto fazaaCreateDto)
        {
            throw new NotImplementedException();
        }

        public FazaaDto GetFazaaById(int id)
        {
            return _context
           .Fazaas
           .Where(dt => dt.Id == id)
           .ProjectTo<FazaaDto>(_mapper.ConfigurationProvider)
           .SingleOrDefault()!;
        }

        public IEnumerable<FazaaDto> GetFazaas()
        {
            return _context
           .Fazaas
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