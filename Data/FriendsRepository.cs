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

        public bool SendFriendRequest(FriendsCreateDto friendCreateDto, int PassengerId)
        {
            Friends friends = new()
            {
                Confirmed = false,
                FriendId = friendCreateDto.FriendId,
                PassengerId = PassengerId
            };
            _context.Friends.Add(friends);

            return SaveChanges();
        }

        public bool ConfirmFriendRequest(int FriendId, int PassengerId)
        {
            var friend = _context
                        .Friends
                        .Where(f => f.FriendId == FriendId && f.PassengerId == PassengerId)
                        .SingleOrDefault();
            friend!.Confirmed = true;
            return SaveChanges();
        }

        public FriendsDto GetFriendById(int id)
        {
            return _context
           .Friends
           .Where(f => f.Id == id && f.Confirmed == true)
           .ProjectTo<FriendsDto>(_mapper.ConfigurationProvider)
           .SingleOrDefault()!;
        }

        public IEnumerable<FriendsDto> GetFriendsById(int id)
        {
            return _context
           .Friends
           .Where(f => f.Id == id && f.Confirmed == true)
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

        public bool DeleteFriend(int FriendId, int PassengerId)
        {
            var friendToDelete = _context
                                .Friends
                                .Where(f => f.FriendId == FriendId && f.PassengerId == PassengerId)
                                .SingleOrDefault();

            if (friendToDelete == null)
                return false;

            _context.Friends.Remove(friendToDelete!);
            return SaveChanges();
        }
    }
}