using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
    public class BusController(
        IBusRepository busRepository,
        IDriverRepository driverRepository

            ) : BaseApiController
    {
        private readonly IBusRepository _busRepository = busRepository;
        private readonly IDriverRepository _driverRepository = driverRepository;

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPost("addBus")]
        public async Task<ActionResult> CreateBus(BusCreateDto busCreateDto)
        {
            var driver = await _driverRepository.GetDriverById(busCreateDto.DriverId);

            if(driver == null)
                return NotFound("Driver Not Found" + busCreateDto.DriverId);

            if(driver.BusId != null)
                return BadRequest("Driver Already Has a Bus");

            var bus = _busRepository.CreateBus(busCreateDto);
            if (await _busRepository.SaveChanges())
                return Created("", bus);

            return StatusCode(500, "Server Error");
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN", "DRIVER")]
        [HttpGet("getBuses")]
        public async Task<ActionResult<IEnumerable<BusDto>>> GetBuses()
        {
            return Ok(await _busRepository.GetBuses());
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN")]
        [HttpGet("getActiveBuses")]
        public async Task<ActionResult<IEnumerable<BusDto>>> GetActiveBuses()
        {
            return Ok(await _busRepository.GetActiveBuses());
        }

        [CustomAuthorize("PASSENGER")]
        [HttpGet("getActiveBusesByRoute/{id}")]
        public async Task<ActionResult<IEnumerable<BusDto>>> getActiveBusesByRoute(int id)
        {
            return Ok(await _busRepository.GetActiveBusesByRoute(id));
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN", "DRIVER")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BusDto?>> GetBusById(int id)
        {
            return await _busRepository.GetBusById(id);
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult> updateBus(BusUpdateDto busUpdateDto, int id)
        {
            var bus = await _busRepository.GetBusById(id);
            var driver = await _driverRepository.GetDriverById(busUpdateDto.DriverId);

            if (bus == null)
                return NotFound("Bus Not Found");

            if(driver == null)
                return NotFound("Driver Not Found" + busUpdateDto.DriverId);

            if(driver.BusId != null)
                return BadRequest("Driver Already Has a Bus");


            if(!await _busRepository.Update(busUpdateDto, id)) 
                return BadRequest("Failed to Update Bus");
            
            if (await _busRepository.SaveChanges())
                return NoContent();

            return BadRequest("No Changes Made");
        }
    }
}