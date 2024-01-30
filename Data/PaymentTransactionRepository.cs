using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PaymentTransactionRepository(DataContext context, IMapper mapper) : IPaymentTransactionRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<PaymentTransactionDto?> CreatePaymentTransaction(PaymentTransactionCreateDto paymentTransactionDto)
        {
            PaymentTransaction paymentTransaction = new()
            {
                Amount = paymentTransactionDto.Amount,
                PassengerId = paymentTransactionDto.PassengerId,
                TripId = paymentTransactionDto.TripId,
                BusId = paymentTransactionDto.BusId,
                RouteId = paymentTransactionDto.RouteId,
                DriverId = paymentTransactionDto.DriverId,
                TimeStamp = DateTime.UtcNow
            };
            await _context.PaymentTransactions.AddAsync(paymentTransaction);

            return _mapper.Map<PaymentTransactionDto>(paymentTransaction);
        }

        public async Task<PaymentTransactionDto?> GetPaymentTransactionById(int id)
        {
            return await _context
            .PaymentTransactions
            .Where(pt => pt.Id == id)
            .ProjectTo<PaymentTransactionDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<PaymentTransactionDto?>> GetPaymentTransactions(int id)
        {
            return await _context
            .PaymentTransactions
            .Where(pt => pt.PassengerId == id)
            .ProjectTo<PaymentTransactionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}