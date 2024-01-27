using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [CustomAuthorize("PASSENGER")]
    public class TripController(
        ITokenHandlerService tokenHandlerService,
        ITripRepository tripRepository
            ) : BaseApiController
    {
        private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;
        private readonly ITripRepository _tripRepository = tripRepository;

        [HttpGet("getTrips")]
        public async Task<ActionResult<IEnumerable<TripDto>>> GetTrips()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            return Ok(await _tripRepository.GetTrips(Id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TripDto?>> GetTripById(int id)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            string role = _tokenHandlerService.ExtractUserRole();
            if (
            role == "Not" || 
            (
            role.ToUpper() != Role.PASSENGER.ToString()
            ))
                return Forbid("Not authorized");

            return await _tripRepository.GetTripById(id, Id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> updateTrip(TripUpdateDto tripUpdateDto, int id)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            var tripDto = await _tripRepository.GetTripById(id, Id);

            if (tripDto == null)
                return NotFound("Trip Not Found");

            _tripRepository.Update(tripUpdateDto, id);

            if (await _tripRepository.SaveChanges())
                return NoContent();

            return BadRequest("No Changes Made");
        }
        [HttpPost]
        public async Task<ActionResult> CreateTrip(TripCreateDto tripCreateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            await _tripRepository.CreateTrip(tripCreateDto, Id);
           if(!await _tripRepository.SaveChanges())
                    return StatusCode(500, "Server Error1");
            
            return StatusCode(201);
        }
    }
}