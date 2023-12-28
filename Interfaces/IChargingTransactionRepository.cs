using API.DTOs;

namespace API.Interfaces
{
    public interface IChargingTransactionRepository
    {
        IEnumerable<ChargingTransactionDto> GetChargingTransactions();
        ChargingTransactionDto GetChargingTransactionById(int id);
        bool CreateChargingTransaction(ChargingTransactionCreateDto chargingTransactionDto);
        bool SaveChanges();
    }
}