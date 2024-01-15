using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class PointRepository : IPointRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PointRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Point CreatePoint(PointCreateDto pointDto)
        {
            Point point = new()
            {
                Name = pointDto.Name,
                Latitude = pointDto.Latitude,
                Longitude = pointDto.Longitude,
                CreatedAt = DateTime.UtcNow
            };
            _context.Points.Add(point);

            return point;
        }

        public PointDto GetPointById(int id)
        {
            return _context
            .Points
            .Where(p => p.Id == id)
            .ProjectTo<PointDto>(_mapper.ConfigurationProvider)
            .SingleOrDefault()!;
        }

        public IEnumerable<PointDto> GetPoints()
        {
            return _context
                .Points
                .ProjectTo<PointDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public PointDto PointExists(double lat, double lon)
        {
            return _context
                    .Points
                    .Where(p => p.Latitude == lat && p.Longitude == lon)
                    .ProjectTo<PointDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefault()!;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(PointDto point)
        {
            _context.Entry(point).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}