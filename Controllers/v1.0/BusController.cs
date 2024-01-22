using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
    public class BusController : BaseApiController
    {
        private readonly IBusRepository _busRepository;

        public BusController(
            IBusRepository busRepository
            )
        {
            _busRepository = busRepository;
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPost("addBus")]
        public ActionResult CreateBus(BusCreateDto busCreateDto)
        {
            _busRepository.CreateBus(busCreateDto);
            if (_busRepository.SaveChanges())
                return NoContent();
            return StatusCode(500, "Server Error");
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN", "DRIVER")]
        [HttpGet("getBuses")]
        public ActionResult<IEnumerable<BusDto>> GetBuses()
        {
            return Ok(_busRepository.GetBuses());
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN")]
        [HttpGet("getActiveBuses")]
        public ActionResult<IEnumerable<BusDto>> GetActiveBuses()
        {
            return Ok(_busRepository.GetActiveBuses());
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN", "DRIVER")]
        [HttpGet("{id}")]
        public ActionResult<BusDto> GetBusById(int id)
        {
            return _busRepository.GetBusById(id);
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPut("{id}")]
        public ActionResult updateBus(BusUpdateDto busUpdateDto, int id)
        {
            var bus = _busRepository.GetBusById(id);

            if (bus == null)
                return NotFound();

            _busRepository.Update(busUpdateDto, id);

            if (_busRepository.SaveChanges())
                return NoContent();

            return BadRequest("Failed to Update Bus");
        }
    }
}