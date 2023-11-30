using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    // [Authorize]
    public class PassengerController : BaseApiController
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IMapper _mapper;

        public PassengerController(IPassengerRepository passengerRepository, IMapper mapper)
        {
            _passengerRepository = passengerRepository;
            _mapper = mapper;
        }

        [HttpGet("GetPassengers")]
        public ActionResult<IEnumerable<PassengerDto>> GetPassengers() => Ok(_passengerRepository.GetPassengers());

        [HttpGet("{id}")]
        public ActionResult<PassengerDto> GetPassengerById(int id) => _passengerRepository.GetPassengerById(id);

        [HttpPut("updatePassenger")]
        public ActionResult updatePassenger(PassengerUpdateDto passengerUpdateDto)
        {
            var Email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var passenger = _passengerRepository.GetPassengerOnlyByEmail(Email);
            var user = _passengerRepository.GetUserById(passenger.UserId);

            if (passenger == null) return NotFound();
            _mapper.Map(passengerUpdateDto, passenger);
            _mapper.Map(passengerUpdateDto.User, user);
            if (_passengerRepository.SaveChanges()) return NoContent();

            return BadRequest("Failed to Update Passenger");
        }

        // [HttpGet("GetOTPs")]
        // public ActionResult<List<OTP>> GetOTPs()
        // {
        //     var otps = _context.Passengers.Include(p => p.OTPs).FirstOrDefault(p => p.Id == 1).OTPs;
        //     return otps;
        // }



    }
}