using API.Validations;

namespace API.Controllers.v1
{
    [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN")]
    public class PaymentTransactionController
    {
        
    }
}