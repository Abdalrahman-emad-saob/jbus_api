using API.Entities;

namespace API.Interfaces
{
    public interface IPaymentTransactionRepository
    {
        void Update(PaymentTransaction paymentTransaction);
        IEnumerable<PaymentTransaction> GetPaymentTransactions();
        PaymentTransaction GetPaymentTransactionById(int id);
        bool SaveChanges();
    }
}