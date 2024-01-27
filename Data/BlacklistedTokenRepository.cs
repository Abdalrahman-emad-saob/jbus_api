using API.Entities;
using API.Interfaces;

namespace API.Data
{
    public class BlacklistedTokenRepository(DataContext context) : IBlacklistedTokenRepository
    {
        private readonly DataContext _context = context;

        public async Task BlacklistTokenAsync(string token)
        {
            BlacklistedToken blacklistedToken = new()
            {
                Token = token
            };

            _context.BlacklistedTokens.Add(blacklistedToken);
            await _context.SaveChangesAsync();
        }
    }
}