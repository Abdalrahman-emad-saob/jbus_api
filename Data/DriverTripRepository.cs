using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DriverTripRepository(
        DataContext context,
        IMapper mapper,
        IDriverRepository driverRepository
    ) : IDriverTripRepository
    {// TODO : check driver id when updating bus
    // TODO : check rpoute id when updating bus too
    // TODO : check payment transaction, the busid, driverid, and routeid are nulls
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IDriverRepository _driverRepository = driverRepository;

        public async Task<(DriverTripDto, string)> CreateDriverTrip(int id, string IsGoing)
        {
            var driver = await _driverRepository.GetDriverById(id);
            if (driver == null)
                return (null!, "Driver not found")!;

            if (driver.Bus == null)
                return (null!, "Driver has no bus")!;

           Enum.TryParse(IsGoing, true, out BusStatus busStatus);
            driver.Bus.Going = busStatus;

            DriverTrip driverTrip = new()
            {
                DriverId = id,
                status = Status.PENDING,
                BusId = driver.BusId,
                RouteId = driver.Bus.RouteId,
                CreatedAt = DateTime.UtcNow,
            };
            await _context.DriverTrips.AddAsync(driverTrip);
            return (_mapper.Map<DriverTripDto>(driverTrip), "Driver trip created");
        }

        public async Task<DriverTripDto?> GetDriverTripById(int id)
        {
            return await _context
           .DriverTrips
           .Where(dt => dt.Id == id)
           .ProjectTo<DriverTripDto>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<DriverTripDto?>> GetDriverTrips()
        {
            return await _context
           .DriverTrips
           .ProjectTo<DriverTripDto>(_mapper.ConfigurationProvider)
           .ToListAsync();
        }

        public async Task<(DriverTripDto, string)> updateDriverTrip(int id, DriverTripUpdateDto driverTripUpdateDto)
        {
            var driver = await _driverRepository.GetDriverById(id);
            if (driver == null)
                return (null, "Driver not found")!;

            if (driver.DriverTrips == null)
                return (null, "Driver has no trips")!;

            var driverTrip = driver.DriverTrips.Find(dt => dt.status == Status.PENDING || dt.status == Status.ONGOING);

            if (driverTrip == null)
                return (null, "Driver has no active trip")!;

            var driverTripDto = _mapper.Map<DriverTripDto>(driverTrip);

            if (Enum.TryParse(driverTripUpdateDto.status!.ToUpper(), out Status parsedStatus))
            {
                driverTrip.status = parsedStatus;
                if (parsedStatus == Status.COMPLETED)
                {
                    driverTrip.FinishedAt = DateTime.UtcNow;
                }
                return (driverTripDto, "Driver trip is updated");
            }
            return (driverTripDto, "Invalid status");
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}