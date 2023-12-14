using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IChargingTransactionRepository
    {
        IEnumerable<ChargingTransaction> GetChargingTransactions();
        ChargingTransaction GetChargingTransactionById(int id);
        bool CreateChargingTransaction(ChargingTransactionCreateDto chargingTransactionDto);
        bool SaveChanges();
    }
}