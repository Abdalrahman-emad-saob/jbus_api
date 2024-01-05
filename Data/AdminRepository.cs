using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AdminRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public AdminDto CreateAdmin(RegisterAdminDto registerAdminDto)
        {
            var user = new User
            {
                Role = Role.SUPER_ADMIN,
                Name = registerAdminDto.Name,
                PhoneNumber = registerAdminDto.PhoneNumber,
                Email = registerAdminDto.Email?.ToLower()
            };
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, registerAdminDto.Password!);
            var admin = new Admin() { };

            _context.Users.Add(user);
            _context.Admins.Add(admin);
            SaveChanges();

            var adminDto = _mapper.Map<AdminDto>(admin);
            return adminDto;
        }

        public Admin GetAdminByEmail(string Email)
        {
            return _context.Admins
                .Where(a => a.User!.Email == Email && a.User.Email.ToLower() == Email.ToLower())
                .SingleOrDefault()!;
        }

        public Admin GetAdminById(int id)
        {
            return _context.Admins
                .Where(a => a.UserId == id)
                .SingleOrDefault()!;
        }

        public AdminDto GetAdminDtoById(int id)
        {
            return _context
           .Admins
           .Where(a => a.Id == id)
           .ProjectTo<AdminDto>(_mapper.ConfigurationProvider)
           .SingleOrDefault()!;
        }

        public IEnumerable<AdminDto> GetAdmins()
        {
            return _context
                .Admins
                .ProjectTo<AdminDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(AdminDto adminDto)
        {
            _context.Entry(adminDto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}