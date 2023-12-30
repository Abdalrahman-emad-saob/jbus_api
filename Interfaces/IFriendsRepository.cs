using API.DTOs;

namespace API.Interfaces
{
    public interface IFriendsRepository
    {
        IEnumerable<FriendsDto> GetFriendsById(int id);
        FriendsDto GetFriendById(int id);
        bool SendFriendRequest(FriendsCreateDto friendCreateDto, int PassengerId);
        bool ConfirmFriendRequest(int FriendId, int PassengerId);
        bool DeleteFriend(int FriendId, int PassengerId);
        void Update(FriendsDto friend);
        bool SaveChanges();
    }
}