using API.Entities;

namespace API.Interfaces
{
    public interface IChargingTransactionRepository
    {
        void Update(ChargingTransaction chargingTransaction);
        IEnumerable<ChargingTransaction> GetChargingTransactions();
        ChargingTransaction GetChargingTransactionById(int id);
        bool SaveChanges();
    }
}