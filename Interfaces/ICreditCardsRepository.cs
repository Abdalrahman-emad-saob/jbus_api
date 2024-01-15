using API.Entities;

namespace API.Interfaces
{
    public interface ICreditCardsRepository
    {
        CreditCard GetCreditCardByCardNumber(long CardNumber);
    }
}