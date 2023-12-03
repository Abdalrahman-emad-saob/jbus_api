using API.Entities;
using API.Interfaces;

namespace API.Data
{
    // TODO OTPs
    public class OTPRepository : IOTPRepository
    {
        private readonly DataContext _context;

        public OTPRepository(DataContext context)
        {
            _context = context;
        }
        public OTP GetOTPById(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

    }
}