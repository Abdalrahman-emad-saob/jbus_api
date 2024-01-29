using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TripRepository(DataContext context, IMapper mapper) : ITripRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<TripDto?> CreateTrip(TripCreateDto tripDto, int PassengerId, int BusId)
        {
            // bool isStatusParsed = Enum.TryParse(tripDto.status, true, out TripStatus status);

            // if (!isStatusParsed)
            // {
            //     throw new ArgumentException("Invalid status");
            // }
            var bus = await _context.Buses.FindAsync(BusId);
            int driverTripId = (await _context.DriverTrips.FindAsync(bus!.DriverTrips!.Find(dt => dt.status == Status.PENDING || dt.status == Status.ONGOING)!.Id))!.Id;
            Point pointPick = new()
            {
                Latitude = tripDto.PickUpPoint!.Latitude,
                Longitude = tripDto.PickUpPoint.Longitude
            };
            // System.Console.WriteLine("||");
            // System.Console.WriteLine("||");
            // System.Console.WriteLine("||");
            // System.Console.WriteLine("||");
            // System.Console.WriteLine("||");
            // System.Console.WriteLine("||");
            // System.Console.WriteLine(tripDto.DropOffPoint);
            Point pointDrop = new();
            if (tripDto.DropOffPoint != null)
                if (tripDto.DropOffPoint.Longitude != 0 && tripDto.DropOffPoint.Latitude != 0)
                {
                    pointDrop = new()
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
                status = TripStatus.PENDING,
                PassengerId = PassengerId,
                PickUpPointId = pointPick.Id,
                StartedAt = DateTime.UtcNow,
                DriverTripId = driverTripId,
            };

            if(tripDto.DropOffPoint != null)
                trip.DropOffPointId = pointDrop.Id;

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

        public async Task Update(TripUpdateDto tripUpdateDto, int id)
        {
            var trip = await _context.Trips.FindAsync(id);

            _mapper.Map(tripUpdateDto, trip);
        }

        public async Task<TripDto?> finishTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id);

            trip!.status = TripStatus.COMPLETED;
            trip.FinishedAt = DateTime.UtcNow;

            return _mapper.Map<TripDto>(trip);
        }
    }
}