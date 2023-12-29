using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers.v1
{
    [Authorize]
    public class PassengerController : BaseApiController
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHandlerService _tokenHandlerService;

        public PassengerController(
            IPassengerRepository passengerRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ITokenHandlerService tokenHandlerService)
        {
            _passengerRepository = passengerRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenHandlerService = tokenHandlerService;
        }

        [HttpGet("getpassengers")]
        public ActionResult<IEnumerable<PassengerDto>> GetPassengers() => Ok(_passengerRepository.GetPassengers());

        [HttpGet]
        public ActionResult<PassengerDto> GetPassengerById()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");
            return _passengerRepository.GetPassengerDtoById(Id);
        }

        [HttpPut]
        public ActionResult updatePassenger(PassengerUpdateDto passengerUpdateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            var passenger = _passengerRepository.GetPassengerById(Id);
            var user = _userRepository.GetUserById((int)passenger.UserId!);

            if (passenger == null || user == null) return NotFound();
            _mapper.Map(passengerUpdateDto, passenger);
            _mapper.Map(passengerUpdateDto.User, user);

            if (_passengerRepository.SaveChanges()) return NoContent();
            return BadRequest("Failed to Update Passenger");
        }
    }
}