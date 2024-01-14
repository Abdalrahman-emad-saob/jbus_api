using API.Controllers.v1;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
    [Authorize]
    public class BusController : BaseApiController
    {
        private readonly IBusRepository _busRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHandlerService _tokenHandlerService;

        public BusController(
            IBusRepository busRepository,
            IMapper mapper,
            ITokenHandlerService tokenHandlerService)
        {
            _busRepository = busRepository;
            _mapper = mapper;
            _tokenHandlerService = tokenHandlerService;
        }
        [HttpPost("addBus")]
        public ActionResult CreateBus(BusCreateDto busCreateDto)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" ||
            (
            role.ToUpper() != Role.SUPER_ADMIN.ToString() &&
            role.ToUpper() != Role.ADMIN.ToString()))
                return Unauthorized("Not authorized");

            _busRepository.CreateBus(busCreateDto);
            return StatusCode(201);
        }
        [HttpGet("getBuses")]
        public ActionResult<IEnumerable<BusDto>> GetBuses()
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" ||
            (
            role.ToUpper() != Role.SUPER_ADMIN.ToString() &&
            role.ToUpper() != Role.ADMIN.ToString() &&
            role.ToUpper() != Role.PASSENGER.ToString()
            ))
                return Unauthorized("Not authorized");

            return Ok(_busRepository.GetBuses());
        }

        [HttpGet("{id}")]
        public ActionResult<BusDto> GetBusById(int id)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (
            role == "Not" ||
            (
            role.ToUpper() != Role.SUPER_ADMIN.ToString() &&
            role.ToUpper() != Role.ADMIN.ToString() &&
            role.ToUpper() != Role.PASSENGER.ToString()
            ))
                return Unauthorized("Not authorized");

            return _busRepository.GetBusById(id);
        }

        [HttpPut("{id}")]
        public ActionResult updateBus(BusUpdateDto busUpdateDto, int id)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (
            role == "Not" || 
            (
            role.ToUpper() != Role.SUPER_ADMIN.ToString() && 
            role.ToUpper() != Role.ADMIN.ToString()
            ))
                return Unauthorized("Not authorized");

            var route = _busRepository.GetBusById(id);

            if (route == null)
                return NotFound();

            _mapper.Map(busUpdateDto, route);

            if (_busRepository.SaveChanges())
                return NoContent();

            return BadRequest("Failed to Update Bus");
        }
    }
}