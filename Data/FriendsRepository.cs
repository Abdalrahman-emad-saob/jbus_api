using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class FriendsRepository : IFriendsRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FriendsRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateFriend(FriendsCreateDto friendCreateDto)
        {
            throw new NotImplementedException();
        }

        public FriendsDto GetFriendById(int id)
        {
            return _context
           .Friends
           .Where(f => f.Id == id)
           .ProjectTo<FriendsDto>(_mapper.ConfigurationProvider)
           .SingleOrDefault()!;
        }

        public IEnumerable<FriendsDto> GetFriendsById(int id)
        {
            return _context
           .Friends
           .Where(f => f.Id == id)
           .ProjectTo<FriendsDto>(_mapper.ConfigurationProvider)
           .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(FriendsDto friend)
        {
            _context.Entry(friend).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}