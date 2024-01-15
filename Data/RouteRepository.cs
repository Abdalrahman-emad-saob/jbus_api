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
        private readonly IInterestPointRepository _interestPointRepository;

        public RouteRepository(DataContext context, IMapper mapper, IInterestPointRepository interestPointRepository)
        {
            _context = context;
            _mapper = mapper;
            _interestPointRepository = interestPointRepository;
        }

        public bool CreateRoute(RouteCreateDto routeDto)
        {
            InterestPointCreateDto interestPointCreateDto1 = new()
            {
                Name = routeDto.StartingPoint!.Name,
                Logo = routeDto.StartingPoint.Logo,
                PointName = routeDto.StartingPoint.PointName,
                Latitude = routeDto.StartingPoint.Latitude,
                Longitude = routeDto.StartingPoint.Longitude
            };
            InterestPointCreateDto interestPointCreateDto2 = new()
            {
                Name = routeDto.EndingPoint!.Name,
                Logo = routeDto.EndingPoint.Logo,
                PointName = routeDto.EndingPoint.PointName,
                Latitude = routeDto.EndingPoint.Latitude,
                Longitude = routeDto.EndingPoint.Longitude
            };
            var interestPoint1 = _interestPointRepository.CreateInterestPoint(interestPointCreateDto1);
            var interestPoint2 = _interestPointRepository.CreateInterestPoint(interestPointCreateDto2);
            Entities.Route route = new()
            {
                Name = routeDto.Name,
                WaypointsGoing = routeDto.WaypointsGoing,
                WaypointsReturning = routeDto.WaypointsReturning,
                StartingPointId = interestPoint1.Id,
                EndingPointId = interestPoint2.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Routes.Add(route);
            
            return SaveChanges();
        }

        public RouteDto GetRouteById(int id)
        {
            return _context.Routes
            .Where(r => r.Id == id)
            .ProjectTo<RouteDto>(_mapper.ConfigurationProvider)
            .SingleOrDefault()!;
        }

        public IEnumerable<RouteDto> GetRoutes() => _context.Routes
                .ProjectTo<RouteDto>(_mapper.ConfigurationProvider)
                .ToList();

        public bool SaveChanges() => _context.SaveChanges() > 0;

        public void Update(RouteDto route) => _context.Entry(route).State = EntityState.Modified;
    }
}