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
                CreatedAt = DateTime.UtcNow,
                FriendId = friendCreateDto.FriendId,
                PassengerId = PassengerId,
                Friend = _context.Passengers.Find(friendCreateDto.FriendId),
                Passenger = _context.Passengers.Find(PassengerId)
            };
            
            _context.Friends.Add(friends);

            return true;
        }

        public bool ConfirmFriendRequest(int Id, int PassengerId)
        {
            var friend = _context
                        .Friends
                        .Where(f => f.Id == Id && f.FriendId == PassengerId)
                        .SingleOrDefault();
            friend!.Confirmed = true;

            return true;
        }

        public FriendsDto GetFriendById(int Id, int PassengerId)
        {
            return _context
                    .Friends
                    .Where(f => f.Confirmed == true && f.Id == Id && (f.PassengerId == PassengerId || f.FriendId == PassengerId))
                    .ProjectTo<FriendsDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefault()!;
        }

        public bool FriendRequestExists(int FriendId, int PassengerId)
        {
            bool friendExists = _context.Friends
                                .Any(f => (f.FriendId == FriendId && f.PassengerId == PassengerId) || (f.FriendId == PassengerId && f.PassengerId == FriendId));

            return friendExists;
        }

        public IEnumerable<FriendsDto> GetFriends(int id)
        {
            return _context
           .Friends
           .Where(f => (f.PassengerId == id || f.FriendId == id) && f.Confirmed == true)
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

        public bool DeleteFriend(int Id, int PassengerId)
        {
            var friendToDelete = _context
                                .Friends
                                .Where(f => f.Id == Id && (f.PassengerId == PassengerId || f.FriendId == PassengerId))
                                .SingleOrDefault();

            _context.Friends.Remove(friendToDelete!);
            return true;
        }

        public IEnumerable<FriendsDto> GetFriendRequests(int PassengerId)
        {
            var friendsRequests = _context
                            .Friends
                            .Where(f => f.FriendId == PassengerId && f.Confirmed == false)
                            .ProjectTo<FriendsDto>(_mapper.ConfigurationProvider)
                            .ToList();
            return friendsRequests;
        }
    }
}