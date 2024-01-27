using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PointRepository(DataContext context, IMapper mapper) : IPointRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Point?> CreatePoint(PointCreateDto pointDto)
        {
            Point point = new()
            {
                Name = pointDto.Name,
                Latitude = pointDto.Latitude,
                Longitude = pointDto.Longitude,
                CreatedAt = DateTime.UtcNow
            };
            await _context.Points.AddAsync(point);

            return point;
        }

        public async Task<PointDto?> GetPointById(int id)
        {
            return await _context
            .Points
            .Where(p => p.Id == id)
            .ProjectTo<PointDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<PointDto?>> GetPoints()
        {
            return await _context
                .Points
                .ProjectTo<PointDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PointDto?> PointExists(double lat, double lon)
        {
            return await _context
                    .Points
                    .Where(p => p.Latitude == lat && p.Longitude == lon)
                    .ProjectTo<PointDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync()!;
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(PointDto point)
        {
            _context.Entry(point).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}