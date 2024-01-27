using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DriverRepository(DataContext context, IMapper mapper) : IDriverRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<DriverDto> CreateDriver(RegisterDriverDto registerDriverDto)
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
            await _context.Users.AddAsync(user);
            await _context.Drivers.AddAsync(driver);
            await SaveChanges();
            var driverDto = _mapper.Map<DriverDto>(driver);
            return driverDto;
        }

        public async Task<Driver?> GetDriverByEmail(string Email)
        {
            return await _context.Drivers
                .Where(p => p.User!.Email == Email)
                .SingleOrDefaultAsync()!;
        }

        public async Task<Driver?> GetDriverById(int id)
        {
            return await _context
            .Drivers
            .Include(d => d.Bus)
                .ThenInclude(b => b!.Route)
            .Include(d => d.DriverTrips)
            .Where(d => d.Id == id)
            .SingleOrDefaultAsync()!;
        }

        public async Task<DriverDto?> GetDriverDtoById(int id)
        {
            return await _context
           .Drivers
           .Where(d => d.Id == id)
           .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<DriverDto>> GetDrivers()
        {
            return await _context
           .Drivers
           .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
           .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(DriverUpdateDto driver)
        {
            _context.Entry(driver).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}