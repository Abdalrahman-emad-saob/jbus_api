using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BlacklistedTokenRepository : IBlacklistedTokenRepository
    {
        private readonly DataContext _context;

        public BlacklistedTokenRepository(DataContext context)
        {
            _context = context;
        }
        public async Task BlacklistTokenAsync(string token)
        {
            BlacklistedToken blacklistedToken = new BlacklistedToken
            {
                Token = token
            };

            _context.BlacklistedTokens.Add(blacklistedToken);
            await _context.SaveChangesAsync();
        }
    }
}