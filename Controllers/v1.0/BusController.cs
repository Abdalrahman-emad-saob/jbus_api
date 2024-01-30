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

            if (driver == null)
                return NotFound(new { Error = "Driver Not Found" + busCreateDto.DriverId });

            // if (driver.BusId != null)
            //     return BadRequest("Driver Already Has a Bus");

            var bus = await _busRepository.CreateBus(busCreateDto);
            if (await _busRepository.SaveChanges())
                return Created("", bus);

            return StatusCode(500, new { Error = "Server Error" });
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPut("De_ActivateBus/{id}")]
        private async Task<ActionResult> De_ActivateBus(int id, bool active)
        {
            var bus = await _busRepository.GetBusById(id);
            if (bus == null)
                return NotFound(new { Error = "Bus Not Found" });

            await _busRepository.De_ActivateBus(id, active);

            if (await _busRepository.SaveChanges())
                return NoContent();

            return StatusCode(500, new { Error = "Server Error" });
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
                return NotFound(new { Error = "Bus Not Found" });

            if (driver == null)
                return NotFound(new { Error = "Driver Not Found" + busUpdateDto.DriverId });

            // if (driver.BusId != null)
            //     return BadRequest("Driver Already Has a Bus");


            if (!await _busRepository.Update(busUpdateDto, id))
                return BadRequest(new { Error = "Failed to Update Bus" });

            if (await _busRepository.SaveChanges())
                return NoContent();

            return BadRequest(new { Error = "No Changes Made" });
        }
    }
}