using API.Entities;

namespace API.Interfaces
{
    public interface ICreditCardsRepository
    {
        Task<CreditCard?> GetCreditCardByCardNumber(long CardNumber);
    }
}