using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class InterestPointRepository : IInterestPointRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPointRepository _pointRepository;

        public InterestPointRepository(DataContext context, IMapper mapper, IPointRepository pointRepository)
        {
            _context = context;
            _mapper = mapper;
            _pointRepository = pointRepository;
        }

        public InterestPoint CreateInterestPoint(InterestPointCreateDto interestPointDto)
        {
            PointCreateDto pointCreateDto = new()
            {
                Name = interestPointDto.Name,
                Latitude = interestPointDto.Latitude,
                Longitude = interestPointDto.Longitude,
                CreatedAt = DateTime.UtcNow
            };
            var point = _pointRepository.CreatePoint(pointCreateDto);
            _context.Points.Add(point);
            SaveChanges();
            InterestPoint interestPoint = new()
            {
                Name = interestPointDto.Name,
                Logo = interestPointDto.Logo,
                LocationId = point.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.InterestPoints.Add(interestPoint);

            return interestPoint;
        }

        public InterestPointDto GetInterestPointById(int id)
        {
            return _context.InterestPoints
                .Where(ip => ip.Id == id)
                .ProjectTo<InterestPointDto>(_mapper.ConfigurationProvider)
                .SingleOrDefault()!;
        }

        public IEnumerable<InterestPointDto> GetInterestPoints()
        {
            return _context
            .InterestPoints
            .ProjectTo<InterestPointDto>(_mapper.ConfigurationProvider)
            .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(InterestPointDto interestPoint)
        {
            _context.Entry(interestPoint).State = EntityState.Modified;
        }
    }
}