using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Data
{
    public class ChargingTransactionRepository : IChargingTransactionRepository
    {
        private readonly DataContext _context;

        public ChargingTransactionRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateChargingTransaction(ChargingTransactionCreateDto chargingTransactionDto)
        {
            int payMethod = 0;
            if(chargingTransactionDto.paymentMethod?.ToLower() == "mastercard")
                payMethod = 0;
            else if(chargingTransactionDto.paymentMethod?.ToLower() == "visa")
                payMethod = 1;
            ChargingTransaction chargingTransaction = new()
            {
                paymentMethod = (ChargingTransaction.PaymentMethod)payMethod,
                Amount = chargingTransactionDto.Amount,
                PassengerId = chargingTransactionDto.PassengerId
            };
            _context.ChargingTransactions.Add(chargingTransaction);
            return SaveChanges();
        }

        public ChargingTransaction GetChargingTransactionById(int id)
        {
            return _context
            .ChargingTransactions
            .Where(ct => ct.Id == id)
            .SingleOrDefault()!;
        }

        public IEnumerable<ChargingTransaction> GetChargingTransactions()
        {
            return _context
                .ChargingTransactions
                .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}