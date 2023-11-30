using API.Entities;

namespace API.Interfaces
{
    public interface IOTPRepository
    {
        void Update(OTP oTP);
        IEnumerable<OTP> GetOTPs();
        OTP GetOTPById(int id);
        bool SaveChanges();
    }
}