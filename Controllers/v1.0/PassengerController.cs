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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PassengerController(IPassengerRepository passengerRepository, IUserRepository userRepository, IMapper mapper)
        {
            _passengerRepository = passengerRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("GetPassengers")]
        public ActionResult<IEnumerable<PassengerDto>> GetPassengers() => Ok(_passengerRepository.GetPassengers());

        [HttpGet("{id}")]
        public ActionResult<PassengerDto> GetPassengerById(int id) => _passengerRepository.GetPassengerDtoById(id);

        [HttpPut("{id}")]
        public ActionResult updatePassenger(int id, PassengerUpdateDto passengerUpdateDto)
        {
            var passenger = _passengerRepository.GetPassengerById(id);
            var user = _userRepository.GetUserById((int)passenger.UserId!);

            if (passenger == null) return NotFound();
            _mapper.Map(passengerUpdateDto, passenger);
            _mapper.Map(passengerUpdateDto.User, user);
            if (_passengerRepository.SaveChanges()) return NoContent();

            return BadRequest("Failed to Update Passenger");
        }

    }
}