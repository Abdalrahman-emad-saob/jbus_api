using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(UserDto userDto);
        UserDto GetUserDtoById(int id);
        User GetUserByEmail(string Email);
        User GetUserById(int id);
    }
}