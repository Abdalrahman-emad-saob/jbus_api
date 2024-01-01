using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class DriverTripRepository : IDriverTripRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DriverTripRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateDriverTrip(DriverTripCreateDto driverTripCreateDto)
        {
            throw new NotImplementedException();
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