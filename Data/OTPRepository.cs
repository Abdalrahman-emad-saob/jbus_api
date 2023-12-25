using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Data
{
    public class OTPRepository : IOTPRepository
    {
        private readonly DataContext _context;

        public OTPRepository(DataContext context)
        {
            _context = context;
        }

        public int CreateOTP(string Email)
        {
            Random random = new();
            int otp = random.Next(1000, 10000);
            OTP oTP = new()
            {
                Otp = otp,
                PassengerEmail = Email
            };
            _context.OTPs.Add(oTP);
            _context.SaveChanges();
            return otp;
        }

        public bool DeleteOTP(int id)
        {
            var otp = _context.OTPs.Find(id);
            if (otp != null)
            {
                _context.OTPs.Remove(otp);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public OTP GetOTPByEmail(string Email)
        {
            return _context.OTPs.Where(otp => otp.PassengerEmail == Email).SingleOrDefault()!;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

    }
}