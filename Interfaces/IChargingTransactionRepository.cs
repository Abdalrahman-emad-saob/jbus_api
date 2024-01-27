using API.DTOs;

namespace API.Interfaces
{
    public interface IChargingTransactionRepository
    {
        Task<IEnumerable<ChargingTransactionDto?>> GetChargingTransactions();
        Task<ChargingTransactionDto?> GetChargingTransactionById(int id);
        Task<bool> CreateChargingTransaction(ChargingTransactionCreateDto chargingTransactionDto, int id);
        Task<bool> SaveChanges();
    }
}