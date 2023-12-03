using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class TripRepository : ITripRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TripRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public TripDto GetTripById(int id)
        {
            return _context
            .Trips
            .Where(t => t.Id == id)
            .ProjectTo<TripDto>(_mapper.ConfigurationProvider)
            .SingleOrDefault();
        }

        public IEnumerable<TripDto> GetTrips()
        {
            return _context
            .Trips
            .ProjectTo<TripDto>(_mapper.ConfigurationProvider)
            .ToList();
        }

        public IEnumerable<TripDto> GetTripsById(int id)
        {
            return _context
            .Trips
            .Where(t => t.PassengerId == id)
            .ProjectTo<TripDto>(_mapper.ConfigurationProvider)
            .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(TripDto trip)
        {
            _context.Entry(trip).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}