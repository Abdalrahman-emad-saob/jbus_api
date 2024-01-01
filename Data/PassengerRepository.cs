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
                .Where(p => p.User!.Email == Email && p.User.Email!.Equals(Email, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefault()!;
        }

        public IEnumerable<PassengerDto> GetPassengers() => _context.Passengers
                .ProjectTo<PassengerDto>(_mapper.ConfigurationProvider)
                .ToList();

        public bool SaveChanges() => _context.SaveChanges() > 0;

        public void Update(PassengerDto passenger) => _context.Entry(passenger).State = EntityState.Modified;

        public LoginResponseDto CreatePassenger(RegisterDto registerDto)
        {
            var user = new User
            {
                Role = Role.PASSENGER,
                Name = registerDto.Name,
                PhoneNumber = registerDto.PhoneNumber,
                Email = registerDto.Email?.ToLower()
            };
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, registerDto.Password!);
            var passenger = new Passenger()
            {
                Wallet = 0,
                User = user
            };
            var token = _tokenService.CreateToken(user, passenger.Id);

            _context.Users.Add(user);
            _context.Passengers.Add(passenger);
            SaveChanges();

            return new LoginResponseDto
            {
                passengerDto = _mapper.Map<PassengerDto>(passenger),
                Token = token
            };
        }
        public User GetUserByEmail(string Email)
        {
            return _context.Users.AsEnumerable()
                    .FirstOrDefault(x => x.Email != null && x.Email.Equals(Email, StringComparison.CurrentCultureIgnoreCase))!;
        }
    }
}