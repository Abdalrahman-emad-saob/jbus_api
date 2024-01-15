using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class PaymentTransactionRepository : IPaymentTransactionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PaymentTransactionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreatePaymentTransaction(PaymentTransactionCreateDto paymentTransactionDto)
        {
            PaymentTransaction paymentTransaction = new()
            {
                Amount = paymentTransactionDto.Amount,
                PassengerId = paymentTransactionDto.PassengerId,
                TripId = paymentTransactionDto.TripId,
                TimeStamp = DateTime.UtcNow
            };
            _context.PaymentTransactions.Add(paymentTransaction);

            return SaveChanges();
        }

        public PaymentTransactionDto GetPaymentTransactionById(int id)
        {
            return _context
            .PaymentTransactions
            .Where(pt => pt.Id == id)
            .ProjectTo<PaymentTransactionDto>(_mapper.ConfigurationProvider)
            .SingleOrDefault()!;
        }

        public IEnumerable<PaymentTransactionDto> GetPaymentTransactions(int id)
        {
            return _context
            .PaymentTransactions
            .Where(pt => pt.PassengerId == id)
            .ProjectTo<PaymentTransactionDto>(_mapper.ConfigurationProvider)
            .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}