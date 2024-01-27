using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PredefinedStopsRepository(DataContext context, IMapper mapper) : IPredefinedStopsRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<PredefinedStopsDto?> CreatePredefinedStops(PredefinedStopsCreateDto predefinedStopsCreateDto)
        {
            PredefinedStops predefinedStops = new()
            {
                RouteId = predefinedStopsCreateDto.RouteId,
                points = _mapper.Map<List<Point>>(predefinedStopsCreateDto.points),
                CreatedAt = predefinedStopsCreateDto.CreatedAt,
                UpdatedAt = predefinedStopsCreateDto.UpdatedAt
            };
            await _context.PredefinedStops.AddAsync(predefinedStops);
            await SaveChanges();
            (await _context.Routes.FindAsync(predefinedStopsCreateDto.RouteId))!.PredefinedStopsId = predefinedStops.Id;
            return _mapper.Map<PredefinedStopsDto>(predefinedStops);
        }

        public async Task<PredefinedStopsDto?> GetPredefinedStopById(int id)
        {
            return await _context
           .PredefinedStops
           .Include(dt => dt.points)
           .Where(dt => dt.RouteId == id && dt.IsActive == ActiveStatus.Active)
           .ProjectTo<PredefinedStopsDto>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<PredefinedStopsDto?>> GetPredefinedStops()
        {
            return await _context
           .PredefinedStops
           .Include(dt => dt.points)
           .Where(dt => dt.IsActive == ActiveStatus.Active)
           .ProjectTo<PredefinedStopsDto>(_mapper.ConfigurationProvider)
           .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}