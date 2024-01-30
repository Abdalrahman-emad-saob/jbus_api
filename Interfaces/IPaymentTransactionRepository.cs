using API.DTOs;

namespace API.Interfaces
{
    public interface IPaymentTransactionRepository
    {
        Task<IEnumerable<PaymentTransactionDto?>> GetPaymentTransactions(int id);
        Task<PaymentTransactionDto?> GetPaymentTransactionById(int id);
        Task<PaymentTransactionDto?> CreatePaymentTransaction(PaymentTransactionCreateDto paymentTransactionDto);
        Task<bool> SaveChanges();
    }
}