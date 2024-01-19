using API.Controllers.v1;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [CustomAuthorize("PASSENGER")]
    public class FazaaController : BaseApiController
    {
        private readonly IFazaaRepository _fazaaRepository;
        private readonly ITokenHandlerService _tokenHandlerService;

        public FazaaController(
            IFazaaRepository fazaaRepository,
            ITokenHandlerService tokenHandlerService)
        {
            _fazaaRepository = fazaaRepository;
            _tokenHandlerService = tokenHandlerService;
        }

        [HttpPost("storeFazaas")]
        public ActionResult CreateFazaas(FazaaCreateDto fazaaCreateDtos)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            bool result;
            try
            {
                result = _fazaaRepository.StoreFazaas(fazaaCreateDtos, Id);
            }
            catch (Exception)
            {
                return BadRequest("Duplicated Record");
            }
            if (!result)
                return StatusCode(500, "Server Error");

            return StatusCode(201);
        }
        [HttpGet("getFazaas")]
        public ActionResult<IEnumerable<FazaaDto>> getFazaas()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            var fazaas = _fazaaRepository.GetFazaas(Id);
            if (fazaas == null)
                return NotFound("No Fazaa Found");

            return Ok(fazaas);
        }
        [HttpGet("getFazaaById/{id}")]
        public ActionResult<FazaaDto> getFazaaById(int id)
        {
            var fazaa = _fazaaRepository.GetFazaaById(id);
            if (fazaa == null)
                return NotFound("Fazaa Not Found");

            return Ok(fazaa);
        }
    }
}