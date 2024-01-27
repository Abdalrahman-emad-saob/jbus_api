using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PassengerRepository(
        DataContext context,
        IMapper mapper
        ) : IPassengerRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<PassengerDto?> GetPassengerDtoByEmail(string Email)
        {
            return await _context.Passengers
                .Where(p => p.User!.Email == Email)
                .ProjectTo<PassengerDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync()!;
        }

        public async Task<PassengerDto?> GetPassengerDtoById(int id)
        {
            return await _context.Passengers
                .Where(p => p.Id == id)
                .ProjectTo<PassengerDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync()!;
        }

        public async Task<Passenger?> GetPassengerById(int id)
        {
            return await _context.Passengers
            .Include(p => p.Trips)
                .Where(p => p.UserId == id)
                .SingleOrDefaultAsync()!;
        }

        public async Task<Passenger?> GetPassengerByEmail(string? Email)
        {
            return await _context.Passengers
                .Where(p => p.User!.Email == Email && p.User.Email!.ToLower() == Email!.ToLower())
                .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<PassengerDto?>> GetPassengers()
        {
            return await _context.Passengers
                .ProjectTo<PassengerDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool Update(PassengerUpdateDto passengerUpdateDto, Passenger passenger, User user)
        {
            _mapper.Map(passengerUpdateDto, passenger);
            _mapper.Map(passengerUpdateDto.User, user);

            _context.Entry(passenger).State = EntityState.Modified;
            return true;
        }

        public async Task<RegisterResponseDto?> CreatePassenger(RegisterDto registerDto)
        {
            var user = new User
            {
                Role = Role.PASSENGER,
                Name = registerDto.Name,
                PhoneNumber = registerDto.PhoneNumber,
                Email = registerDto.Email?.ToLower(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastActive = DateTime.UtcNow
            };
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, registerDto.Password!);
            var passenger = new Passenger()
            {
                Wallet = 0,
                User = user,
                FcmToken = registerDto.FCMToken
            };

            await _context.Users.AddAsync(user);
            await _context.Passengers.AddAsync(passenger);

            var passengerDto = _mapper.Map<PassengerDto>(passenger);
            return new RegisterResponseDto
            {
                user = user,
                passengerDto = passengerDto
            };
        }

        public async Task UpdateRewardPoints(int rp, int id)
        {
            var passenger = await _context.Passengers
                .Where(p => p.Id == id)
                .SingleOrDefaultAsync()!;

            passenger!.RewardPoints += rp;
        }

        public async Task UpdateRewardPointsToAll(int rp)
        {
            var passengers = await _context.Passengers
                .ToListAsync()!;

            foreach (var passenger in passengers)
            {
                passenger.RewardPoints += rp;
            }
        }

    }
}