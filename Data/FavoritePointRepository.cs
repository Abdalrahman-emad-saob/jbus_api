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

        public bool DeleteFavoritePoint(int id, int Id)
        {
            var favoritePoint = _context.FavoritePoints
            .Where(fp => fp.Id == id && fp.PassengerId == Id)
            .SingleOrDefault();

            if (favoritePoint != null)
            {
                _context.FavoritePoints.Remove(favoritePoint);
                return true;
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

        public bool InsertFavoritePoint(FavoritePointCreateDto favoritePointCreateDto, int id)
        {
            var point = _context
            .Points
            .Where(p => p.Latitude == favoritePointCreateDto.Lat && p.Longitude == favoritePointCreateDto.Long)
            .SingleOrDefault();

            if (point == null)
            {
                Point createPoint = new()
                {
                    Latitude = favoritePointCreateDto.Lat,
                    Longitude = favoritePointCreateDto.Long,
                    Name = favoritePointCreateDto.Name,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Points.Add(createPoint);
                SaveChanges();
                FavoritePoint favoritePoint = new()
                {
                    PointId = createPoint.Id,
                    RouteId = favoritePointCreateDto.RouteId,
                    PassengerId = id,
                    CreatedAt = DateTime.UtcNow
                };
                _context.FavoritePoints.Add(favoritePoint);

                return true;
            }
            FavoritePoint createfavoritePoint = new()
            {
                PointId = point.Id,
                RouteId = favoritePointCreateDto.RouteId,
                PassengerId = id,
                CreatedAt = DateTime.UtcNow
            };
            _context.FavoritePoints.Add(createfavoritePoint);
            return true;
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