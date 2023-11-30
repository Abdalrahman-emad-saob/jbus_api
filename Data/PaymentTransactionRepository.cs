using API.Entities;
using API.Interfaces;

namespace API.Data
{
    public class PaymentTransactionRepository : IPaymentTransactionRepository
    {
        public PaymentTransaction GetPaymentTransactionById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PaymentTransaction> GetPaymentTransactions()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(PaymentTransaction paymentTransaction)
        {
            throw new NotImplementedException();
        }
    }
}