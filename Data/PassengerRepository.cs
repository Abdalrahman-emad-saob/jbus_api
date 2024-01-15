using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public PassengerRepository(DataContext context,
                                   IMapper mapper,
                                   ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public PassengerDto GetPassengerDtoByEmail(string Email)
        {
            return _context.Passengers
                .Where(p => p.User!.Email == Email)
                .ProjectTo<PassengerDto>(_mapper.ConfigurationProvider)
                .SingleOrDefault()!;
        }

        public PassengerDto GetPassengerDtoById(int id)
        {
            return _context.Passengers
                .Where(p => p.Id == id)
                .ProjectTo<PassengerDto>(_mapper.ConfigurationProvider)
                .SingleOrDefault()!;
        }

        public Passenger GetPassengerById(int id)
        {
            return _context.Passengers
                .Where(p => p.UserId == id)
                .SingleOrDefault()!;
        }

        public Passenger GetPassengerByEmail(string? Email)
        {
            return _context.Passengers
                .Where(p => p.User!.Email == Email && p.User.Email!.ToLower() == Email!.ToLower())
                .SingleOrDefault()!;
        }

        public IEnumerable<PassengerDto> GetPassengers() => _context.Passengers
                .ProjectTo<PassengerDto>(_mapper.ConfigurationProvider)
                .ToList();

        public bool SaveChanges() => _context.SaveChanges() > 0;

        public void Update(PassengerDto passenger)
        {
            _context.Entry(passenger).State = EntityState.Modified;
        }

        public RegisterResponseDto CreatePassenger(RegisterDto registerDto)
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
                User = user
            };

            _context.Users.Add(user);
            _context.Passengers.Add(passenger);
            SaveChanges();

            var passengerDto = _mapper.Map<PassengerDto>(passenger);
            return new RegisterResponseDto
            {
                user = user,
                passengerDto = passengerDto
            };
        }

    }
}