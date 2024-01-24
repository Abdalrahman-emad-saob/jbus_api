using API.DTOs;
using API.Entities;
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
                Latitude = routeDto.StartingPoint.Latitude,
                Longitude = routeDto.StartingPoint.Longitude,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            InterestPointCreateDto interestPointCreateDto2 = new()
            {
                Name = routeDto.EndingPoint!.Name,
                Logo = routeDto.EndingPoint.Logo,
                Latitude = routeDto.EndingPoint.Latitude,
                Longitude = routeDto.EndingPoint.Longitude,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            var interestPoint1 = _interestPointRepository.CreateInterestPoint(interestPointCreateDto1);
            var interestPoint2 = _interestPointRepository.CreateInterestPoint(interestPointCreateDto2);
            SaveChanges();
            Entities.Route route = new()
            {
                Name = routeDto.Name,
                WaypointsGoing = routeDto.WaypointsGoing,
                WaypointsReturning = routeDto.WaypointsReturning,
                Fee = routeDto.Fee,
                StartingPointId = interestPoint1.Id,
                EndingPointId = interestPoint2.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Routes.Add(route);
            
            return true;
        }

        public RouteDto GetRouteById(int id)
        {
            return _context
            .Routes
            .Where(r => r.Id == id && r.IsActive == ActiveStatus.Active)
            .ProjectTo<RouteDto>(_mapper.ConfigurationProvider)
            .SingleOrDefault()!;
        }

        public IEnumerable<RouteDto> GetRoutes()
        {
            return _context.Routes
                .Where(r => r.IsActive == ActiveStatus.Active)
                .ProjectTo<RouteDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public bool Update(RouteUpdateDto routeUpdateDto, int id)
        {
            var route = _context.Routes.Find(id);
            _mapper.Map(routeUpdateDto, route);
            route!.UpdatedAt = DateTime.UtcNow;
            // _context.Entry(route).State = EntityState.Modified;
            return true;
        }

        public bool Delete(int id)
        {
            var route = _context.Routes.Find(id);
            var predefinedStops = _context.PredefinedStops.Where(ps => ps.RouteId == id).SingleOrDefault()!;
            route!.IsActive = ActiveStatus.Deleted;
            route!.UpdatedAt = DateTime.UtcNow;
            if (predefinedStops != null)
            {
                predefinedStops.IsActive = ActiveStatus.Deleted;
                predefinedStops.UpdatedAt = DateTime.UtcNow;
            }
            return true;
        }
    }
}