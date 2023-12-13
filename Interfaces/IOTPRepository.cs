using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IOTPRepository
    {
        // TODO
        OTP GetOTPByEmail(string Email);
        int CreateOTP(string Email);
        bool SaveChanges();
        bool DeleteOTP(int id);
    }
}