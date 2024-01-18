using API.Controllers.v1;
using API.DTOs;
using API.Entities;
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
                return Unauthorized("Not authorized1");

            string role = _tokenHandlerService.ExtractUserRole();
            if (
            role == "Not" ||
            (
            role.ToUpper() != Role.PASSENGER.ToString()
            ))
                return Unauthorized("Not authorized2");
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

            string role = _tokenHandlerService.ExtractUserRole();
            if (
            role == "Not" || 
            (
            role.ToUpper() != Role.PASSENGER.ToString()
            ))
                return Unauthorized("Not authorized2");

            var fazaas = _fazaaRepository.GetFazaas(Id);
            if (fazaas == null)
                return NotFound("No Fazaa Found");

            return Ok(fazaas);
        }
        [HttpGet("getFazaaById/{id}")]
        public ActionResult<FazaaDto> getFazaaById(int id)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (
            role == "Not" || 
            (
            role.ToUpper() != Role.PASSENGER.ToString()
            ))
                return Unauthorized("Not authorized");

            var fazaa = _fazaaRepository.GetFazaaById(id);
            if (fazaa == null)
                return NotFound("Fazaa Not Found");

            return Ok(fazaa);
        }
    }
}