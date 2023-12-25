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
            int chargeMethod = 0;
            if(chargingTransactionDto.paymentMethod?.ToLower() == "mastercard")
                chargeMethod = 0;
            else if(chargingTransactionDto.paymentMethod?.ToLower() == "visa")
                chargeMethod = 1;
            ChargingTransaction chargingTransaction = new()
            {
                chargingMethod = (ChargingTransaction.ChargingMethod)chargeMethod,
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