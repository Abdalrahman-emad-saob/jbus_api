using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class FazaaController : BaseApiController
    {
        private readonly IFazaaRepository _fazaaRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHandlerService _tokenHandlerService;

        public FazaaController(
            IFazaaRepository fazaaRepository,
            IMapper mapper,
            ITokenHandlerService tokenHandlerService)
        {
            _fazaaRepository = fazaaRepository;
            _mapper = mapper;
            _tokenHandlerService = tokenHandlerService;
        }

        [HttpPost("storeFazaas")]
        public ActionResult CreateFazaas(IEnumerable<FazaaCreateDto> fazaaCreateDtos)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            bool result = _fazaaRepository.StoreFazaas(fazaaCreateDtos, Id);
            if (!result)
                return StatusCode(500, "Internal Server Error");

            return NoContent();
        }
        [NonAction]
        [HttpGet("getFazaas")]
        public ActionResult<IEnumerable<FazaaDto>> getFazaas()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            var fazaas = _fazaaRepository.GetFazaas(Id);
            if (fazaas == null)
                return NotFound("No Fazaa Found");

            return Ok(fazaas);
        }
        [NonAction]
        [HttpGet("getFazaaById")]
        public ActionResult<FazaaDto> getFazaaById(int Id)
        {
            var fazaa = _fazaaRepository.GetFazaaById(Id);
            if (fazaa == null)
                return NotFound("Fazaa Not Found");

            return Ok(fazaa);
        }
    }
}