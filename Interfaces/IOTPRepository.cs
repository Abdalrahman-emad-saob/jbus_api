using API.Entities;

namespace API.Interfaces
{
    public interface IOTPRepository
    {
        Task<OTP?> GetOTPByEmail(string Email);
        Task<int> CreateOTP(string Email);
        Task<bool> SaveChanges();
        Task<bool> DeleteOTP(int id);
    }
}