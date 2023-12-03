using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public User GetUserById(int id)
        {
            return _context.Users
                .Where(u => u.Id == id)
                .SingleOrDefault();
        }

        public UserDto GetUserDtoById(int id)
        {
            return _context.Users
                .Where(u => u.Id == id)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
        }

        public void Update(UserDto userDto)
        {
            _context.Entry(userDto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}