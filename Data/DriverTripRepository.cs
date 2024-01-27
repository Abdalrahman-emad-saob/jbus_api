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
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IDriverRepository _driverRepository = driverRepository;

        public async Task<(DriverTripDto, string)> CreateDriverTrip(int id)
        {
            var driver = await _driverRepository.GetDriverById(id);
            if (driver == null)
                return (null!, "Driver not found")!;

            if (driver.Bus == null)
                return (null!, "Driver has no bus")!;
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
            var driverTrip = await _context.DriverTrips.FindAsync(id);

            if (driverTrip == null)
                return (null, "Driver trip not found")!;

            if (driverTripUpdateDto.status != null)
            {
                if (driverTrip.status == Status.COMPLETED)
                    return (null, "Trip is Completed")!;
                else if (driverTrip.status == Status.CANCELED)
                    return (null, "Trip is Canceled")!;
            }
            else
                return (null, "Invalid status1")!;

            var driverTripDto = _mapper.Map<DriverTripDto>(driverTrip);

            if (Enum.TryParse(driverTripUpdateDto.status!.ToUpper(), out Status parsedStatus))
            {
                driverTrip.status = parsedStatus;
                if (parsedStatus == Status.COMPLETED)
                {
                    driverTrip.FinishedAt = DateTime.UtcNow;
                    driverTrip.Rating = driverTripUpdateDto.Rating;
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