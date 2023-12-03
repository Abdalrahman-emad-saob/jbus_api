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
        public FavoritePointDto GetFavoritePointById(int id)
        {
             return _context
            .FavoritePoints
            .Where(fp => fp.Id == id)
            .ProjectTo<FavoritePointDto>(_mapper.ConfigurationProvider)
            .SingleOrDefault();
        }

        public IEnumerable<FavoritePointDto> GetFavoritePoints()
        {
            return _context
            .FavoritePoints
            .ProjectTo<FavoritePointDto>(_mapper.ConfigurationProvider)
            .ToList();
        }

        public bool SaveChanges()
        {
            return _context
            .SaveChanges() > 0;
        }

        public void Update(FavoritePointDto favoritePoint)
        {
            _context.Entry(favoritePoint).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}