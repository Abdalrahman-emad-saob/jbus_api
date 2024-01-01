using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IAdminRepository
    {
        void Update(AdminDto AdminDto);
        IEnumerable<AdminDto> GetAdmins();
        AdminDto GetAdminDtoById(int id);
        bool CreateAdmin(AdminCreateDto AdminCreateDto);
        Admin GetAdminById(int id);
        bool SaveChanges();
    }
}