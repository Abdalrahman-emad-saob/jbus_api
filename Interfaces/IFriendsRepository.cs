using API.DTOs;

namespace API.Interfaces
{
    public interface IFriendsRepository
    {
        Task<IEnumerable<FriendsDto?>> GetFriends(int id);
        Task<FriendsDto?> GetFriendById(int id, int PassengerId);
        Task<bool> SendFriendRequest(FriendsCreateDto friendCreateDto, int PassengerId);
        Task<bool> FriendRequestExists(int FriendId, int PassengerId);
        Task<bool> ConfirmFriendRequest(int FriendId, int PassengerId);
        Task<IEnumerable<FriendsDto?>> GetFriendRequests(int PassengerId);
        Task<bool> DeleteFriend(int FriendId, int PassengerId);
        void Update(FriendsDto friend);
        Task<bool> SaveChanges();
    }
}