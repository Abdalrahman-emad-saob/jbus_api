using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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
        public bool CreateAdmin(AdminCreateDto adminCreateDto)
        {
            Admin admin = new() { };
            User user = new()
            {
                Name = adminCreateDto.Name,
                PhoneNumber = adminCreateDto.PhoneNumber,
                Email = adminCreateDto.Email,
                Role = Role.ADMIN,
                Sex = Sex.MALE,
                UpdatedAt = DateTime.UtcNow,
                LastActive = DateTime.UtcNow
            };
            admin.User = user;

            _context.Users.Add(user);
            _context.Admins.Add(admin);

            return SaveChanges();
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