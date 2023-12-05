using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IOTPRepository
    {// TODO
        OTP GetOTPById(int id);
        bool CreateOTP(OTPCreateDto oTP);
        bool SaveChanges();
    }
}