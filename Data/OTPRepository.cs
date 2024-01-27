using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class OTPRepository(
        DataContext context
            ) : IOTPRepository
    {
        private readonly DataContext _context = context;

        public async Task<int> CreateOTP(string Email)
        {
            Random random = new();
            int otp = random.Next(1000, 10000);
            OTP oTP = new()
            {
                Otp = otp,
                PassengerEmail = Email,
                CreatedAt = DateTime.UtcNow
            };
            await _context.OTPs.AddAsync(oTP);
            await _context.SaveChangesAsync();
            return otp;
        }

        public async Task<bool> DeleteOTP(int id)
        {
            var otp = _context.OTPs.Find(id);
            if (otp != null)
            {
                _context.OTPs.Remove(otp);
                return await SaveChanges();
            }
            return false;
        }

        public async Task<OTP?> GetOTPByEmail(string? Email)
        {
            return await _context.OTPs
                    .Where(otp => otp.PassengerEmail == Email)
                    .OrderByDescending(otp => otp.CreatedAt)
                    .FirstOrDefaultAsync()!;
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}