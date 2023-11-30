using API.Entities;
using API.Interfaces;

namespace API.Data
{
    public class ChargingTransactionRepository : IChargingTransactionRepository
    {
        public ChargingTransaction GetChargingTransactionById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChargingTransaction> GetChargingTransactions()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(ChargingTransaction chargingTransaction)
        {
            throw new NotImplementedException();
        }
    }
}