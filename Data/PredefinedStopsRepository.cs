using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class PredefinedStopsRepository : IPredefinedStopsRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PredefinedStopsRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreatePredefinedStop(PredefinedStopsCreateDto predefinedStopsCreateDto)
        {
            throw new NotImplementedException();
        }

        public PredefinedStopsDto GetPredefinedStopById(int id)
        {
            return _context
           .PredefinedStops
           .Where(dt => dt.Id == id)
           .ProjectTo<PredefinedStopsDto>(_mapper.ConfigurationProvider)
           .SingleOrDefault()!;
        }

        public IEnumerable<PredefinedStopsDto> GetPredefinedStops()
        {
            return _context
           .PredefinedStops
           .ProjectTo<PredefinedStopsDto>(_mapper.ConfigurationProvider)
           .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}