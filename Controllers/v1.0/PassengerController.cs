using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Validations;

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
        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpGet("getPassengers")]
        public ActionResult<IEnumerable<PassengerDto>> GetPassengers()
        {
            return Ok(_passengerRepository.GetPassengers());
        }
        [CustomAuthorize("PASSENGER")]
        [HttpGet]
        public ActionResult<PassengerDto> GetPassenger()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            return _passengerRepository.GetPassengerDtoById(Id);
        }

        [CustomAuthorize("PASSENGER")]
        [HttpPut]
        public ActionResult updatePassenger(PassengerUpdateDto passengerUpdateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            var passenger = _passengerRepository.GetPassengerById(Id);
            var user = _userRepository.GetUserById((int)passenger.UserId!);

            if (passenger == null || user == null)
                return NotFound();
            _mapper.Map(passengerUpdateDto, passenger);
            _mapper.Map(passengerUpdateDto.User, user);

            if (_passengerRepository.SaveChanges())
                return NoContent();
            return BadRequest("Failed to Update Passenger");
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPut("{id}")]
        public ActionResult updatePassenger(PassengerUpdateDto passengerUpdateDto, int Id)
        {
            var passenger = _passengerRepository.GetPassengerById(Id);
            var user = _userRepository.GetUserById((int)passenger.UserId!);
            if (passenger == null || user == null || passengerUpdateDto.User == null)
                return NotFound();
            if(passengerUpdateDto.User.Sex != null)
                passengerUpdateDto.User.Sex = passengerUpdateDto.User.Sex.ToUpper();
            if (user.Email != passengerUpdateDto.User.Email)
                if (UserExists(passengerUpdateDto.User.Email))
                {
                    return BadRequest("Email Duplicated");
                }
            _mapper.Map(passengerUpdateDto, passenger);
            _mapper.Map(passengerUpdateDto.User, user);

            if (_passengerRepository.SaveChanges())
                return NoContent();
                
            return BadRequest("Failed to Update Passenger");
        }
        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpGet("{id}")]
        public ActionResult<PassengerDto> GetPassengerById(int id)
        {
            var passenger = _passengerRepository.GetPassengerDtoById(id);
            if (passenger == null)
                return NotFound("Passenger Does not Exist");

            return Ok(passenger);
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPost("AddPassenger")]
        public ActionResult AddPassenger(RegisterDto registerDto)
        {
            if (UserExists(registerDto.Email))
            {
                return BadRequest("Passenger Exists");
            }

            _passengerRepository.CreatePassenger(registerDto);
            
            return StatusCode(201);
        }
        [NonAction]
        private bool UserExists(string? Email)
        {
            return _userRepository.GetUserByEmail(Email!) != null;
        }
    }
}