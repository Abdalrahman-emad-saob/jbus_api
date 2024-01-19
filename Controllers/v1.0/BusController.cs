using API.Controllers.v1;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
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

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPost("addBus")]
        public ActionResult CreateBus(BusCreateDto busCreateDto)
        {
            _busRepository.CreateBus(busCreateDto);
            return StatusCode(201);
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN")]
        [HttpGet("getBuses")]
        public ActionResult<IEnumerable<BusDto>> GetBuses()
        {
            return Ok(_busRepository.GetBuses());
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN")]
        [HttpGet("{id}")]
        public ActionResult<BusDto> GetBusById(int id)
        {
            return _busRepository.GetBusById(id);
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPut("{id}")]
        public ActionResult updateBus(BusUpdateDto busUpdateDto, int id)
        {
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