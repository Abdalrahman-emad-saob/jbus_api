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
        public ChargingTransaction GetChargingTransactionById(int id)
        {
            return _context
            .ChargingTransactions
            .Where(ct => ct.Id == id)
            .SingleOrDefault();
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