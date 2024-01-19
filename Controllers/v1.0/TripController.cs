using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [CustomAuthorize("PASSENGER")]
    public class TripController : BaseApiController
    {
        private readonly ITokenHandlerService _tokenHandlerService;
        private readonly ITripRepository _tripRepository;

        public TripController(
            ITokenHandlerService tokenHandlerService,
            ITripRepository tripRepository
            )
        {
            _tokenHandlerService = tokenHandlerService;
            _tripRepository = tripRepository;
        }
        [HttpGet("getTrips")]
        public ActionResult<IEnumerable<TripDto>> GetTrips()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            return Ok(_tripRepository.GetTrips(Id));
        }

        [HttpGet("{id}")]
        public ActionResult<TripDto> GetTripById(int id)
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

            return _tripRepository.GetTripById(id, Id);
        }
        [HttpPut("{id}")]
        public IActionResult updateTrip(TripUpdateDto tripUpdateDto, int id)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            var tripDto = _tripRepository.GetTripById(id, Id);

            if (tripDto == null)
                return NotFound("Trip Not Found");

            _tripRepository.Update(tripUpdateDto, id);

            if (_tripRepository.SaveChanges())
                return NoContent();

            return BadRequest("No Changes Made");
        }
        [HttpPost]
        public ActionResult CreateTrip(TripCreateDto tripCreateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            _tripRepository.CreateTrip(tripCreateDto, Id);
           if(!_tripRepository.SaveChanges())
                    return StatusCode(500, "Server Error1");
            
            return StatusCode(201);
        }
    }
}