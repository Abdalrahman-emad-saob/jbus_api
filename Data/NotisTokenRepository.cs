using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class NotisTokenRepository(
        DataContext context
        ) : INotisTokenRepository
    {
        private readonly DataContext _context = context;

        public async Task<string?> GetDeviceToken(int? passengerId)
        {
            var passenger = await _context.Passengers.FindAsync(passengerId);
            if(passenger == null)
                return null;
                
            return passenger.FcmToken;
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> StoreDeviceToken(int Id, string deviceToken)
        {
            var passenger = await _context.Passengers.FindAsync(Id);

            if (passenger == null)
                return false;

            passenger.FcmToken = deviceToken;
            await SaveChanges();
            return true;
        }

        public async Task<List<string?>> GetDeviceTokens()
        {
            return await _context.Passengers
            .Where(p => p.FcmToken != null)
            .Select(p => p.FcmToken)
            .ToListAsync();
        }
    }
}