using API.DTOs;
using API.Entities;
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

        public TripDto CreateTrip(TripCreateDto tripDto, int PassengerId)
        {
            int status = 0;
            if (tripDto.status?.ToLower() == "pending")
                status = 0;
            else if (tripDto.status?.ToLower() == "ongoing")
                status = 1;
            else if (tripDto.status?.ToLower() == "completed")
                status = 2;
            else if (tripDto.status?.ToLower() == "canceled")
                status = 3;
            Trip trip = new()
            {
                status = (TripStatus)status,
                PassengerId = PassengerId,
                PickUpPointId = tripDto.PickUpPointId,
                FinishedAt = tripDto.FinishedAt
            };              
            
            _context.Trips.Add(trip);

            return _mapper.Map<TripDto>(trip);
        }

        public TripDto GetTripById(int id, int PassengerId)
        {
            return _context
                .Trips
                .Where(t => t.Id == id && t.PassengerId == PassengerId)
                .ProjectTo<TripDto>(_mapper.ConfigurationProvider)
                .SingleOrDefault()!;
        }

        public IEnumerable<TripDto> GetTrips(int PassengerId)
        {
            return _context
            .Trips
            .Where(t => t.PassengerId == PassengerId)
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

        public void Update(TripUpdateDto tripUpdateDto, int id)
        {
            var trip = _context.Trips.Find(id);
            _mapper.Map(tripUpdateDto, trip);
        }
    }
}