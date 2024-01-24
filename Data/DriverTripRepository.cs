using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class DriverTripRepository : IDriverTripRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;

        public DriverTripRepository(
            DataContext context, 
            IMapper mapper,
            IDriverRepository driverRepository
            )
        {
            _context = context;
            _mapper = mapper;
            _driverRepository = driverRepository;
        }

        public DriverTripDto CreateDriverTrip(DriverTripCreateDto driverTripCreateDto, int id)
        {
            var driver = _driverRepository.GetDriverById(id);
            if (driver.Bus == null)
                return null!;
            DriverTrip driverTrip = new()
            {
                DriverId = id,
                status = Status.PENDING,
                BusId = driver.BusId,
                RouteId = driver.Bus.RouteId,
                CreatedAt = DateTime.UtcNow,
            };
            return _mapper.Map<DriverTripDto>(driverTrip);
        }

        public DriverTripDto GetDriverTripById(int id)
        {
            return _context
           .DriverTrips
           .Where(dt => dt.Id == id)
           .ProjectTo<DriverTripDto>(_mapper.ConfigurationProvider)
           .SingleOrDefault()!;
        }

        public IEnumerable<DriverTripDto> GetDriverTrips()
        {
            return _context
           .DriverTrips
           .ProjectTo<DriverTripDto>(_mapper.ConfigurationProvider)
           .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}