using API.DTOs;

namespace API.Interfaces
{
    public interface IPaymentTransactionRepository
    {
        IEnumerable<PaymentTransactionDto> GetPaymentTransactions(int id);
        PaymentTransactionDto GetPaymentTransactionById(int id);
        bool CreatePaymentTransaction(PaymentTransactionCreateDto paymentTransactionDto);
        bool SaveChanges();
    }
}