using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CreditCardsRepository(DataContext context) : ICreditCardsRepository
    {
        private readonly DataContext _context = context;

        public async Task<CreditCard?> GetCreditCardByCardNumber(long CardNumber)
        {
            return await _context
            .CreditCards
            .Where(cc => cc.CardNumber == CardNumber)
            .SingleOrDefaultAsync()!;
        }
    }
}