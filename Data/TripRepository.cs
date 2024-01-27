using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TripRepository(DataContext context, IMapper mapper) : ITripRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<TripDto?> CreateTrip(TripCreateDto tripDto, int PassengerId)
        {
            // int status = 0;
            // if (tripDto.status?.ToLower() == "pending")
            //     status = 0;
            // else if (tripDto.status?.ToLower() == "ongoing")
            //     status = 1;
            // else if (tripDto.status?.ToLower() == "completed")
            //     status = 2;
            // else if (tripDto.status?.ToLower() == "canceled")
            //     status = 3;
            bool isStatusParsed = Enum.TryParse(tripDto.status, true, out TripStatus status);

            if (!isStatusParsed)
            {
                throw new ArgumentException("Invalid status");
            }
            Point pointPick = new()
            {
                Latitude = tripDto.PickUpPoint!.Latitude,
                Longitude = tripDto.PickUpPoint.Longitude
            };
            if (tripDto.DropOffPoint != null)
                if (tripDto.DropOffPoint.Longitude != 0 && tripDto.DropOffPoint.Longitude != 0)
                {
                    Point pointDrop = new()
                    {
                        Latitude = tripDto.DropOffPoint!.Latitude,
                        Longitude = tripDto.DropOffPoint.Longitude
                    };
                    await _context.Points.AddAsync(pointDrop);
                }
            await _context.Points.AddAsync(pointPick);
            await SaveChanges();
            Trip trip = new()
            {
                status = status,
                PassengerId = PassengerId,
                PickUpPointId = pointPick.Id,
                StartedAt = tripDto.StartedAt
            };

            await _context.Trips.AddAsync(trip);

            return _mapper.Map<TripDto>(trip);
        }

        public async Task<TripDto?> GetTripById(int id, int PassengerId)
        {
            return await _context
                .Trips
                .Where(t => t.Id == id && t.PassengerId == PassengerId)
                .ProjectTo<TripDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<TripDto?>> GetTrips(int PassengerId)
        {
            return await _context
            .Trips
            .Where(t => t.PassengerId == PassengerId)
            .ProjectTo<TripDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<IEnumerable<TripDto?>> GetTripsById(int id)
        {
            return await _context
            .Trips
            .Where(t => t.PassengerId == id)
            .ProjectTo<TripDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(TripUpdateDto tripUpdateDto, int id)
        {
            var trip = _context.Trips.Find(id);
            _mapper.Map(tripUpdateDto, trip);
        }
    }
}