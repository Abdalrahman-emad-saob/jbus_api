using API.Entities;
using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    public class CreditCardsRepository : ICreditCardsRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CreditCardsRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public CreditCard GetCreditCardByCardNumber(long CardNumber)
        {
            return _context
            .CreditCards
            .Where(cc => cc.CardNumber == CardNumber)
            .SingleOrDefault()!;
        }
    }
}