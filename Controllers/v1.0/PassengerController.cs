using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Entities;

namespace API.Controllers.v1
{
    [Authorize]
    public class PassengerController : BaseApiController
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly ITokenHandlerService _tokenHandlerService;

        public PassengerController(
            IPassengerRepository passengerRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ITokenService tokenService,
            ITokenHandlerService tokenHandlerService)
        {
            _passengerRepository = passengerRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _tokenHandlerService = tokenHandlerService;
        }

        [HttpGet("getPassengers")]
        public ActionResult<IEnumerable<PassengerDto>> GetPassengers() => Ok(_passengerRepository.GetPassengers());

        [HttpGet]
        public ActionResult<PassengerDto> GetPassenger()
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

            return _passengerRepository.GetPassengerDtoById(Id);
        }

        [HttpPut]
        public ActionResult updatePassenger(PassengerUpdateDto passengerUpdateDto)
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
        [HttpPut("{id}")]
        public ActionResult updatePassenger(PassengerUpdateDto passengerUpdateDto, int Id)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || 
            (
            role.ToUpper() != Role.SUPER_ADMIN.ToString() && 
            role.ToUpper() != Role.ADMIN.ToString()
            ))
                return Unauthorized("Not authorized");

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
        [HttpGet("{id}")]
        public ActionResult<PassengerDto> GetPassengerById(int id)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || 
            (
            role.ToUpper() != Role.SUPER_ADMIN.ToString() && 
            role.ToUpper() != Role.ADMIN.ToString()
            ))
                return Unauthorized("Not authorized");

            var passenger = _passengerRepository.GetPassengerDtoById(id);
            if (passenger == null)
                return NotFound("Passenger Does not Exist");

            return Ok(passenger);
        }
        [HttpPost("AddPassenger")]
        public ActionResult AddPassenger(RegisterDto registerDto)
        {
            if (UserExists(registerDto.Email))
            {
                return BadRequest("Passenger Exists");
            }

            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || 
            (
            role.ToUpper() != Role.SUPER_ADMIN.ToString() && 
            role.ToUpper() != Role.ADMIN.ToString()
            ))
                return Unauthorized("Not authorized");

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