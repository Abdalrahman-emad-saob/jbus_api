using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BusRepository(
        DataContext context,
        IMapper mapper,
        IDriverRepository driverRepository
    ) : IBusRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IDriverRepository _driverRepository = driverRepository;

        public async Task<bool> CreateBus(BusCreateDto busCreateDto)
        {
            Bus bus = new()
            {
                BusNumber = busCreateDto.BusNumber,
                RouteId = busCreateDto.RouteId,
                DriverId = busCreateDto.DriverId,
                Capacity = busCreateDto.Capacity,
                IsActive = true,
                Going = BusStatus.Idle,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _context.Buses.AddAsync(bus);
            await SaveChanges();
            (await _driverRepository.GetDriverById(busCreateDto.DriverId))!.BusId = bus.Id;
            return true;
        }

        public async Task<BusDto?> GetBusById(int id)
        {
            return await _context
           .Buses
           .Include(b => b.Driver)
              .Include(b => b.Route)
           .Where(d => d.Id == id)
           .ProjectTo<BusDto>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<BusDto>> GetBuses()
        {
            return await _context
                .Buses
                .Include(b => b.Driver)
                    .Include(b => b.Route)
                .ProjectTo<BusDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        public async Task<IEnumerable<BusDto>> GetActiveBuses()
        {
            return await _context
                .Buses
                .Include(b => b.Driver)
                .Include(b => b.Route)
                .ProjectTo<BusDto>(_mapper.ConfigurationProvider)
                .Where(b => b.IsActive == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<BusDto>> GetActiveBusesByRoute(int id)
        {
            return await _context
                .Buses
                .Include(b => b.Driver)
                .Include(b => b.Route)
                .Where(b => (b.Going == BusStatus.Going || b.Going == BusStatus.Returning) && b.RouteId == id)
                .ProjectTo<BusDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(BusUpdateDto busUpdateDto, int id)
        {
            var bus = await _context.Buses.FindAsync(id);
            if (bus == null)
                return false;

            _mapper.Map(busUpdateDto, bus);
            bus.UpdatedAt = DateTime.UtcNow;

            return true;
        }
        public async Task<bool> IsBusActive(int? id)
        {
            var bus = await _context.Buses.FindAsync(id);
            if (bus == null)
                return false;

            return bus.IsActive;
        }

        public async Task<bool> De_ActivateBus(int? id, bool active)
        {
            var bus = await _context.Buses.FindAsync(id);
            if (bus == null)
                return false;
            bus.IsActive = active;
            return bus.IsActive;
        }
    }
}