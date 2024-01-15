using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class FavoritePointRepository : IFavoritePointRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FavoritePointRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool DeleteFavoritePoint(int id)
        {
            var favoritePoint = _context.FavoritePoints.Find(id);
            if (favoritePoint != null)
            {
                _context.FavoritePoints.Remove(favoritePoint);
                return SaveChanges();
            }
            return false;
        }

        public FavoritePointDto GetFavoritePointById(int id)
        {
            return _context
           .FavoritePoints
           .Where(fp => fp.Id == id)
           .ProjectTo<FavoritePointDto>(_mapper.ConfigurationProvider)
           .SingleOrDefault()!;
        }

        public IEnumerable<FavoritePointDto> GetFavoritePoints(int id)
        {
            return _context
            .FavoritePoints
            .Where(fp => fp.PassengerId == id)
            .ProjectTo<FavoritePointDto>(_mapper.ConfigurationProvider)
            .ToList();
        }

        public IEnumerable<FavoritePointDto> GetRouteFavoritePointDtos(int PassengerId, int RouteId)
        {
            return _context
                .FavoritePoints
                .Where(fp => fp.PassengerId == PassengerId && fp.RouteId == RouteId)
                .ProjectTo<FavoritePointDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public bool InsertFavoritePoint(FavoritePointCreateDto favoritePointCreateDto)
        {
            var point = _context.Points.Where(p => p.Latitude == favoritePointCreateDto.Lat).SingleOrDefault();
            if (point == null)
            {
                Point createPoint = new()
                {
                    Latitude = favoritePointCreateDto.Lat,
                    Longitude = favoritePointCreateDto.Long,
                    Name = favoritePointCreateDto.Name,
                    CreatedAt = DateTime.UtcNow
                };
                FavoritePoint favoritePoint = new()
                {
                    PointId = createPoint.Id,
                    RouteId = favoritePointCreateDto.RouteId
                };
                _context.SaveChanges();
                return true;
            }
            FavoritePoint createfavoritePoint = new()
            {
                PointId = point.Id,
                RouteId = favoritePointCreateDto.RouteId
            };
            return SaveChanges();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(FavoritePointDto favoritePoint)
        {
            _context.Entry(favoritePoint).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}