using API.Entities;

namespace API.Interfaces
{
    public interface IOTPRepository
    {
        OTP GetOTPById(int id);
        bool SaveChanges();
    }
}