using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class FriendsRepository(DataContext context, IMapper mapper) : IFriendsRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> SendFriendRequest(FriendsCreateDto friendCreateDto, int PassengerId)
        {
            Friends friends = new()
            {
                Confirmed = false,
                CreatedAt = DateTime.UtcNow,
                FriendId = friendCreateDto.FriendId,
                PassengerId = PassengerId,
                Friend = await _context.Passengers.FindAsync(friendCreateDto.FriendId),
                Passenger = await _context.Passengers.FindAsync(PassengerId)
            };
            
            await _context.Friends.AddAsync(friends);

            return true;
        }

        public async Task<bool> ConfirmFriendRequest(int Id, int PassengerId)
        {
            var friend = await _context
                        .Friends
                        .Where(f => f.PassengerId == Id && f.FriendId == PassengerId)
                        .SingleOrDefaultAsync();
            friend!.Confirmed = true;

            return true;
        }

        public async Task<FriendsDto?> GetFriendById(int Id, int PassengerId)
        {
            return await _context
                    .Friends
                    .Where(f => f.Confirmed == true && f.Id == Id && (f.PassengerId == PassengerId || f.FriendId == PassengerId))
                    .ProjectTo<FriendsDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync()!;
        }

        public async Task<bool> FriendRequestExists(int FriendId, int PassengerId)
        {
            bool friendExists = await _context.Friends
                                .AnyAsync(f => (f.FriendId == FriendId && f.PassengerId == PassengerId) || (f.FriendId == PassengerId && f.PassengerId == FriendId));

            return friendExists;
        }

        public async Task<IEnumerable<FriendsDto?>> GetFriends(int id)
        {
            return await _context
           .Friends
           .Where(f => (f.PassengerId == id || f.FriendId == id) && f.Confirmed == true)
           .ProjectTo<FriendsDto>(_mapper.ConfigurationProvider)
           .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(FriendsDto friend)
        {
            _context.Entry(friend).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task<bool> DeleteFriend(int Id, int PassengerId)
        {
            var friendToDelete = await _context
                                .Friends
                                .Where(f => f.Id == Id && (f.PassengerId == PassengerId || f.FriendId == PassengerId))
                                .SingleOrDefaultAsync();

            _context.Friends.Remove(friendToDelete!);
            return true;
        }

        public async Task<IEnumerable<FriendsDto?>> GetFriendRequests(int PassengerId)
        {
            var friendsRequests = await _context
                            .Friends
                            .Where(f => f.FriendId == PassengerId && f.Confirmed == false)
                            .ProjectTo<FriendsDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
            return friendsRequests;
        }
    }
}