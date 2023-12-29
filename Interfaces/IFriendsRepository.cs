using API.DTOs;

namespace API.Interfaces
{
    public interface IFriendsRepository
    {
        IEnumerable<FriendsDto> GetFriendsById(int id);
        FriendsDto GetFriendById(int id);
        bool CreateFriend(FriendsCreateDto friendCreateDto);
        void Update(FriendsDto friend);
        bool SaveChanges();
    }
}