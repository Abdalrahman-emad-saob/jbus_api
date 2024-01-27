using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(UserDto userDto);
        Task<UserDto?> GetUserDtoById(int id);
        Task<User?> GetUserByEmail(string Email);
        Task<User?> GetUserById(int id);
    }
}