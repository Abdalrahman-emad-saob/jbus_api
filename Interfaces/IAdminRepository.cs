using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IAdminRepository
    {
        void Update(int id);
        IEnumerable<AdminDto> GetAdmins();
        AdminDto GetAdminDtoById(int id);
        AdminDto CreateAdmin(RegisterAdminDto registerAdminDto);
        Admin GetAdminById(int id);
        Admin GetAdminByEmail(string Email);
        bool SaveChanges();
    }
}