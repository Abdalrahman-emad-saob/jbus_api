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

        public bool CreateTrip(TripCreateDto tripDto, int PassengerId)
        {
            int status = 0;
            if(tripDto.status?.ToLower() == "pending")
                status = 0;
            else if(tripDto.status?.ToLower() == "ongoing")
                status = 1;
            else if(tripDto.status?.ToLower() == "completed")
                status = 2;
            else if(tripDto.status?.ToLower() == "canceled")
                status = 3;
            Trip trip = new()
            {
                status = (TripStatus)status,
                PassengerId = PassengerId,
                PaymentTransactionId = tripDto.PaymentTransactionId,
                PickUpPointId = tripDto.PickUpPointId,
                DropOffPointId = tripDto.DropOffPointId
            };
            _context.Trips.Add(trip);

            return SaveChanges();
        }

        public TripDto GetTripById(int id)
        {
            return _context
                .Trips
                .Where(t => t.Id == id)
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

        public void Update(TripDto trip)
        {
            _context.Entry(trip).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}