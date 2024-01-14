using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
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

            string role = _tokenHandlerService.ExtractUserRole();
            if (
            role == "Not" || 
            (
            role.ToUpper() != Role.PASSENGER.ToString()
            ))
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
                return Unauthorized("Not authorized");
            // TODO Give Passnger id for further valdiation
            return _tripRepository.GetTripById(id);
        }
        // TODO Update Trip
        // [HttpPut]
        // public ActionResult updateTrip(PassengerUpdateDto passengerUpdateDto)
        // {
        //     int Id = _tokenHandlerService.TokenHandler();
        //     if (Id == -1)
        //         return Unauthorized("Not authorized");

        //     string role = _tokenHandlerService.ExtractUserRole();
        //     if (role == "Not" || role.ToUpper() != Role.PASSENGER.ToString())
        //         return Unauthorized("Not authorized");

        //     var passenger = _passengerRepository.GetPassengerById(Id);
        //     var user = _userRepository.GetUserById((int)passenger.UserId!);

        //     if (passenger == null || user == null)
        //         return NotFound();
        //     _mapper.Map(passengerUpdateDto, passenger);
        //     _mapper.Map(passengerUpdateDto.User, user);

        //     if (_passengerRepository.SaveChanges())
        //         return NoContent();
        //     return BadRequest("Failed to Update Passenger");
        // }
        [HttpPost("CreateTrip")]
        public ActionResult CreateTrip(TripCreateDto tripCreateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || 
            (
            role.ToUpper() != Role.SUPER_ADMIN.ToString() && 
            role.ToUpper() != Role.ADMIN.ToString()
            ))
                return Unauthorized("Not authorized");

            _tripRepository.CreateTrip(tripCreateDto, Id);
            
            return StatusCode(201);
        }
    }
}