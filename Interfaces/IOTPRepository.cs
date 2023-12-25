using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IOTPRepository
    {
        OTP GetOTPByEmail(string Email);
        int CreateOTP(string Email);
        bool SaveChanges();
        bool DeleteOTP(int id);
    }
}