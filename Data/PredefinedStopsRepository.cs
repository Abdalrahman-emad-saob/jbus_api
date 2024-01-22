using API.DTOs;
using API.Entities;
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

        public PredefinedStopsDto CreatePredefinedStops(PredefinedStopsCreateDto predefinedStopsCreateDto)
        {
            PredefinedStops predefinedStops = new()
            {
                RouteId = predefinedStopsCreateDto.RouteId,
                points = _mapper.Map<List<Point>>(predefinedStopsCreateDto.points),
                CreatedAt = predefinedStopsCreateDto.CreatedAt,
                UpdatedAt = predefinedStopsCreateDto.UpdatedAt
            };

            _context.PredefinedStops.Add(predefinedStops);
            return _mapper.Map<PredefinedStopsDto>(predefinedStops);
        }

        public PredefinedStopsDto GetPredefinedStopById(int id)
        {
            return _context
           .PredefinedStops
           .Where(dt => dt.RouteId == id)
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