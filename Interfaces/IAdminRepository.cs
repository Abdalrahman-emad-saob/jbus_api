using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IAdminRepository
    {
        Task<bool> Update(int id);
        Task<IEnumerable<AdminDto?>> GetAdmins();
        Task<AdminDto?> GetAdminDtoById(int id);
        Task<AdminDto?> CreateAdmin(RegisterAdminDto registerAdminDto);
        Task<Admin?> GetAdminById(int id);
        Task<Admin?> GetAdminByEmail(string Email);
        Task<bool> SaveChanges();
    }
}