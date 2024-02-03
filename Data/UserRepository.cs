using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .SingleOrDefaultAsync()!;
        }

        public async Task<UserDto?> GetUserDtoById(int id)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync()!;
        }

        public void Update(UserDto userDto)
        {
            _context.Entry(userDto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task<User?> GetUserByEmail(string Email)
        {
            return await _context
                        .Users
                        .FirstOrDefaultAsync(x => x.Email != null && EF.Functions.Like(x.Email, Email));
        }
    }
}