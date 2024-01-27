using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class InterestPointRepository(DataContext context, IMapper mapper, IPointRepository pointRepository) : IInterestPointRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IPointRepository _pointRepository = pointRepository;

        public async Task<InterestPoint?> CreateInterestPoint(InterestPointCreateDto interestPointDto)
        {
            PointCreateDto pointCreateDto = new()
            {
                Name = interestPointDto.Name,
                Latitude = interestPointDto.Latitude,
                Longitude = interestPointDto.Longitude,
                CreatedAt = DateTime.UtcNow
            };
            var point = await _pointRepository.CreatePoint(pointCreateDto);
            await _context.Points.AddAsync(point!);
            await SaveChanges();
            InterestPoint interestPoint = new()
            {
                Name = interestPointDto.Name,
                Logo = interestPointDto.Logo,
                LocationId = point!.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _context.InterestPoints.AddAsync(interestPoint);

            return interestPoint;
        }

        public async Task<InterestPointDto?> GetInterestPointById(int id)
        {
            return await _context.InterestPoints
                .Where(ip => ip.Id == id)
                .ProjectTo<InterestPointDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<InterestPointDto?>> GetInterestPoints()
        {
            return await _context
            .InterestPoints
            .ProjectTo<InterestPointDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(InterestPointDto interestPoint)
        {
            _context.Entry(interestPoint).State = EntityState.Modified;
        }
    }
}