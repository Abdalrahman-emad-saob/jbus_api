using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AdminRepository(DataContext context, IMapper mapper) : IAdminRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<AdminDto?> CreateAdmin(RegisterAdminDto registerAdminDto)
        {
            var user = new User
            {
                Role = Role.SUPER_ADMIN,
                Name = registerAdminDto.Name,
                PhoneNumber = registerAdminDto.PhoneNumber,
                Email = registerAdminDto.Email?.ToLower(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastActive = DateTime.UtcNow
            };
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, registerAdminDto.Password!);
            var admin = new Admin
            {
                User = user,
                UserId = user.Id
            };
            await _context.Users.AddAsync(user);
            await _context.Admins.AddAsync(admin);
            await SaveChanges();

            var adminDto = _mapper.Map<AdminDto>(admin);
            return adminDto;
        }

        public async Task<Admin?> GetAdminByEmail(string Email)
        {
            return await _context.Admins
                .Where(a => a.User!.Email == Email && a.User.Email.ToLower() == Email.ToLower())
                .SingleOrDefaultAsync()!;
        }

        public async Task<Admin?> GetAdminById(int id)
        {
            return await _context
                .Admins
                .Where(a => a.Id == id)
                .SingleOrDefaultAsync()!;
        }

        public async Task<AdminDto?> GetAdminDtoById(int id)
        {
            return await _context
           .Admins
           .Where(a => a.Id == id)
           .ProjectTo<AdminDto>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<AdminDto?>> GetAdmins()
        {
            return await _context
                .Admins
                .ProjectTo<AdminDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
                if (admin.User != null)
                {
                    admin.User.UpdatedAt = DateTime.UtcNow;
                    admin.User.LastActive = DateTime.UtcNow;
                    _context.Entry(admin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    return true;
                }
            return false;
        }
    }
}