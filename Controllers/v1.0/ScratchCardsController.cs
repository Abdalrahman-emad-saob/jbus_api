using API.Controllers.v1;
using API.DTOs;
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
    [HttpPost]
    public async Task<ActionResult<ScratchCardDto?>> CreateCard(ScratchCardCreateDto scratchCardCreateDto)
    {
        var scratchCard = await _scratchCardRepository.CreateCard(scratchCardCreateDto);
        if (scratchCard == null) return BadRequest(new { Error = "Failed to create card" });
        return Ok(scratchCard);
    }
    [CustomAuthorize("PASSENGER")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ScratchCardDto?>> ChargeCard(int id)
    {
        int Id = _tokenHandlerService.TokenHandler();
        var sc = await _scratchCardRepository.GetScratchCardById(id);

        if (sc == null) return BadRequest(new { Error = $"Card does not exist {id}" });

        if (sc.Status == "USED") return BadRequest(new { Error = $"Card has been used {id}" });

        if (sc.ExpiryDate < DateTime.UtcNow) return BadRequest(new { Error = $"Card has expired" });

        var scratchCard = await _scratchCardRepository.ChargeCard(id, Id);
        if (scratchCard == null) return BadRequest(new { Error = "Failed to charge passenger" });

        if (!await _scratchCardRepository.SaveChanges())
            return BadRequest(new { Error = "Failed to charge passenger" });

        return Ok(scratchCard);
    }

}