using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DriverRepository : IDriverRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DriverRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public DriverDto CreateDriver(RegisterDriverDto registerDriverDto)
        {
            var user = new User
            {
                Role = Role.SUPER_ADMIN,
                Name = registerDriverDto.Name,
                PhoneNumber = registerDriverDto.PhoneNumber,
                Email = registerDriverDto.Email?.ToLower(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastActive = DateTime.UtcNow
            };
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, registerDriverDto.Password!);
            var driver = new Driver
            {
                User = user
            };
            _context.Users.Add(user);
            _context.Drivers.Add(driver);
            SaveChanges();
            var driverDto = _mapper.Map<DriverDto>(driver);
            return driverDto;
        }

        public Driver GetDriverByEmail(string Email)
        {
            return _context.Drivers
                .Where(p => p.User!.Email == Email)
                .SingleOrDefault()!;
        }

        public Driver GetDriverById(int id)
        {
            return _context
            .Drivers
            .Include(d => d.Bus)
                .ThenInclude(b => b!.Route)
            .Include(d => d.DriverTrips)
            .Where(d => d.Id == id)
            .SingleOrDefault()!;
        }

        public DriverDto GetDriverDtoById(int id)
        {
            return _context
           .Drivers
           .Where(d => d.Id == id)
           .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
           .SingleOrDefault()!;
        }

        public IEnumerable<DriverDto> GetDrivers()
        {
            return _context
           .Drivers
           .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
           .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(DriverDto driver)
        {
            _context.Entry(driver).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}