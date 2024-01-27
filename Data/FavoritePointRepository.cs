using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class FavoritePointRepository(DataContext context, IMapper mapper) : IFavoritePointRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> DeleteFavoritePoint(int id, int Id)
        {
            var favoritePoint = await _context.FavoritePoints
            .Where(fp => fp.Id == id && fp.PassengerId == Id)
            .SingleOrDefaultAsync();

            if (favoritePoint != null)
            {
                _context.FavoritePoints.Remove(favoritePoint);
                return true;
            }
            return false;
        }

        public async Task<FavoritePointDto?> GetFavoritePointById(int id)
        {
            return await _context
           .FavoritePoints
           .Where(fp => fp.Id == id)
           .ProjectTo<FavoritePointDto>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<FavoritePointDto?>> GetFavoritePoints(int id)
        {
            return await _context
            .FavoritePoints
            .Where(fp => fp.PassengerId == id)
            .ProjectTo<FavoritePointDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<IEnumerable<FavoritePointDto?>> GetRouteFavoritePointDtos(int PassengerId, int RouteId)
        {
            return await _context
                .FavoritePoints
                .Where(fp => fp.PassengerId == PassengerId && fp.RouteId == RouteId)
                .ProjectTo<FavoritePointDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> InsertFavoritePoint(FavoritePointCreateDto favoritePointCreateDto, int id)
        {
            var point = await _context
            .Points
            .Where(p => p.Latitude == favoritePointCreateDto.Lat && p.Longitude == favoritePointCreateDto.Long)
            .SingleOrDefaultAsync();

            if (point == null)
            {
                Point createPoint = new()
                {
                    Latitude = favoritePointCreateDto.Lat,
                    Longitude = favoritePointCreateDto.Long,
                    Name = favoritePointCreateDto.Name,
                    CreatedAt = DateTime.UtcNow
                };
                await _context.Points.AddAsync(createPoint);
                await SaveChanges();
                FavoritePoint favoritePoint = new()
                {
                    PointId = createPoint.Id,
                    RouteId = favoritePointCreateDto.RouteId,
                    PassengerId = id,
                    CreatedAt = DateTime.UtcNow
                };
                await _context.FavoritePoints.AddAsync(favoritePoint);

                return true;
            }
            FavoritePoint createfavoritePoint = new()
            {
                PointId = point.Id,
                RouteId = favoritePointCreateDto.RouteId,
                PassengerId = id,
                CreatedAt = DateTime.UtcNow
            };
            await _context.FavoritePoints.AddAsync(createfavoritePoint);
            return true;
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(FavoritePointDto favoritePoint)
        {
            _context.Entry(favoritePoint).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}