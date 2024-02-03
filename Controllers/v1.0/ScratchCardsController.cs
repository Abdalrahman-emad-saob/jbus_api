using API.Controllers.v1;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ScratchCardsController(IScratchCardRepository scratchCardRepository, ITokenHandlerService tokenHandlerService) : BaseApiController
{
    private readonly IScratchCardRepository _scratchCardRepository = scratchCardRepository;
    private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;
    [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScratchCardDto?>>> GetScratchCards()
    {
        var scratchCards = await _scratchCardRepository.GetScratchCards();
        return Ok(scratchCards);
    }
    [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
    [HttpGet("statuses")]
    public ActionResult<string[]> GetScratchCardsStatuses()
    {
        var scratchCardsStatuses = _scratchCardRepository.GetScratchCardsStatuses();
        return Ok(scratchCardsStatuses);
    }
    [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
    [HttpPost("{number}")]
    public async Task<ActionResult<ScratchCardDto?>> CreateCard(int number, ScratchCardCreateDto scratchCardCreateDto)
    {
        try
        {
            var scratchCard = await _scratchCardRepository.CreateCard(scratchCardCreateDto, number);
            if (scratchCard == false) return BadRequest(new { Error = "Failed to create cards" });
            if (!await _scratchCardRepository.SaveChanges())
                return BadRequest(new { Error = "Failed to create cards" });
            return Created("", new { Success = "Cards created successfully" });
        }
        catch (Exception)
        {
            return BadRequest(new { Error = "Error while creating cards" });
        }
    }
    [CustomAuthorize("PASSENGER")]
    [HttpPut("chargeUsingSC/{CN}")]
    public async Task<ActionResult<ScratchCardDto?>> ChargeCard(int CN)
    {
        int Id = _tokenHandlerService.TokenHandler();
        var sc = await _scratchCardRepository.GetScratchCardByCN(CN);

        if (sc == null) return BadRequest(new { Error = $"Card does not exist {CN}" });

        if (sc.Status == ScratchCardStatus.USED.ToString()) return BadRequest(new { Error = $"Card has been used {CN}" });

        if (sc.ExpiryDate < DateTime.UtcNow) return BadRequest(new { Error = $"Card has expired" });

        var scratchCard = await _scratchCardRepository.ChargeCard(CN, Id);
        if (scratchCard == null) return BadRequest(new { Error = "Failed to charge passenger" });

        if (!await _scratchCardRepository.SaveChanges())
            return BadRequest(new { Error = "Failed to charge passenger" });

        return Ok(scratchCard);
    }

}