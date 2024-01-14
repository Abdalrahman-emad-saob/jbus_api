using API.DTOs;

namespace API.Interfaces
{
    public interface IFriendsRepository
    {
        IEnumerable<FriendsDto> GetFriends(int id);
        FriendsDto GetFriendById(int id, int PassengerId);
        bool SendFriendRequest(FriendsCreateDto friendCreateDto, int PassengerId);
        bool FriendRequestExists(int FriendId, int PassengerId);
        bool ConfirmFriendRequest(int FriendId, int PassengerId);
        IEnumerable<FriendsDto> GetFriendRequests(int PassengerId);
        bool DeleteFriend(int FriendId, int PassengerId);
        void Update(FriendsDto friend);
        bool SaveChanges();
    }
}