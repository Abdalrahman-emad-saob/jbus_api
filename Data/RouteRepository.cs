using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class RouteRepository(DataContext context, IMapper mapper, IInterestPointRepository interestPointRepository) : IRouteRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IInterestPointRepository _interestPointRepository = interestPointRepository;

        public async Task<bool> CreateRoute(RouteCreateDto routeDto)
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
            var interestPoint1 = await _interestPointRepository.CreateInterestPoint(interestPointCreateDto1);
            var interestPoint2 = await _interestPointRepository.CreateInterestPoint(interestPointCreateDto2);
            await SaveChanges();
            Entities.Route route = new()
            {
                Name = routeDto.StartingPoint!.Name + " - " + routeDto.EndingPoint!.Name,
                WaypointsGoing = routeDto.WaypointsGoing,
                WaypointsReturning = routeDto.WaypointsReturning,
                Fee = routeDto.Fee,
                StartingPointId = interestPoint1!.Id,
                EndingPointId = interestPoint2!.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _context.Routes.AddAsync(route);

            return true;
        }

        public async Task<RouteDto?> GetRouteById(int id)
        {
            return await _context
            .Routes
            .Where(r => r.Id == id && r.IsActive == ActiveStatus.Active)
            .ProjectTo<RouteDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<RouteDto?>> GetRoutes()
        {
            return await _context.Routes
                .Where(r => r.IsActive == ActiveStatus.Active)
                .ProjectTo<RouteDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(RouteUpdateDto routeUpdateDto, int id)
        {
            var route = await _context.Routes.FindAsync(id);
            _mapper.Map(routeUpdateDto, route);
            route!.UpdatedAt = DateTime.UtcNow;
            // _context.Entry(route).State = EntityState.Modified;
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            var predefinedStops = await _context.PredefinedStops.Where(ps => ps.RouteId == id).SingleOrDefaultAsync()!;
            route!.IsActive = ActiveStatus.Deleted;
            route!.UpdatedAt = DateTime.UtcNow;
            if (predefinedStops != null)
            {
                predefinedStops.IsActive = ActiveStatus.Deleted;
                predefinedStops.UpdatedAt = DateTime.UtcNow;
            }
            return true;
        }

        public async Task<RouteDto?> GetDriverRoute(int id)
        {
            return await _context.Routes
                .Where(r => r.IsActive == ActiveStatus.Active && r.Buses!.Where(b => b.DriverId == id).SingleOrDefault() != null)
                .ProjectTo<RouteDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync()!;
        }
    }
}