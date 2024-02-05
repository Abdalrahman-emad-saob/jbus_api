using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
    [CustomAuthorize("PASSENGER")]
    public class ChargingBalanceController(
        ITokenHandlerService tokenHandlerService,
        IChargingTransactionRepository chargingTransactionRepository,
        ICreditCardsRepository creditCardsRepository
            ) : BaseApiController
    {
        private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;
        private readonly IChargingTransactionRepository _chargingTransactionRepository = chargingTransactionRepository;
        private readonly ICreditCardsRepository _creditCardsRepository = creditCardsRepository;

        [HttpPost("ChargeWallet")]
        public async Task<ActionResult> ChargeWallet(ChargingTransactionCreateDto chargingTransactionCreateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            var creditCard = await _creditCardsRepository.GetCreditCardByCardNumber(chargingTransactionCreateDto.CardNumber);
            if (creditCard == null)
                return NotFound(new { Error = "No Matching Card Found, Best Luck Next Time" });
            if (creditCard.CardType != chargingTransactionCreateDto.paymentMethod)
                return BadRequest(new { Error = "Wrong Card Type" });
            if (creditCard.CVC != chargingTransactionCreateDto.CVC)
                return BadRequest(new { Error = "CVC is not Correct, Stealing Credit Cards? eh?" });
            if (creditCard.ExpirationDate < DateOnly.FromDateTime(DateTime.UtcNow))
                return BadRequest(new { Error = "Card Expired, go outside once and renew it" });
            if (creditCard.ExpirationDate != chargingTransactionCreateDto.ExpirationDate)
                return BadRequest(new { Error = "Bad Thief Catch him ... or her i don't know" });
            if (creditCard.Balance < chargingTransactionCreateDto.Amount)
                return BadRequest(new { Error = "Insuffucient Balance, so POOOOOOOOOR!" });
            // if((await _fazaaRepository.GetFazaaByPassengerId(Id)) != null)
            if (!await _chargingTransactionRepository.CreateChargingTransaction(chargingTransactionCreateDto, Id))
                return StatusCode(500, new { Error = "Server Error1" });
            if (!await _chargingTransactionRepository.SaveChanges())
                return StatusCode(500, new { Error = "Server Error2" });

            return StatusCode(201, new { Success = "Charged Successfully, Lucky Booooy" });
        }
    }
}