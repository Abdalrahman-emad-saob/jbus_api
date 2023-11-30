using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class RouteRepository : IRouteRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RouteRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public RouteDto GetRouteById(int id)
        {
            return _context.Routes.Where(r => r.Id == id).ProjectTo<RouteDto>(_mapper.ConfigurationProvider).SingleOrDefault();
        }

        public IEnumerable<RouteDto> GetRoutes() => _context.Routes
                .ProjectTo<RouteDto>(_mapper.ConfigurationProvider)
                .ToList();

        public bool SaveChanges() => _context.SaveChanges() > 0;

        public void Update(RouteDto route) => _context.Entry(route).State = EntityState.Modified;
    }
}