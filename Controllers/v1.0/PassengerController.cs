using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Validations;

namespace API.Controllers.v1
{
    public class PassengerController(
        IPassengerRepository passengerRepository,
        IUserRepository userRepository,
        ITokenHandlerService tokenHandlerService) : BaseApiController
    {
        private readonly IPassengerRepository _passengerRepository = passengerRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpGet("getPassengers")]
        public async Task<ActionResult<IEnumerable<PassengerDto>>> GetPassengers()
        {
            return Ok(await _passengerRepository.GetPassengers());
        }
        [CustomAuthorize("PASSENGER")]
        [HttpGet]
        public async Task<ActionResult<PassengerDto?>> GetPassenger()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            return await _passengerRepository.GetPassengerDtoById(Id);
        }

        [CustomAuthorize("PASSENGER")]
        [HttpPut]
        public async Task<ActionResult> updatePassenger(PassengerUpdateDto passengerUpdateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            var passenger = await _passengerRepository.GetPassengerById(Id);
            if (passenger == null)
                return NotFound("Passenger Does not Exist");
            var user = await _userRepository.GetUserById((int)passenger.UserId!);

            if (passenger == null || user == null)
                return NotFound();

            _passengerRepository.Update(passengerUpdateDto, passenger, user);

            if (await _passengerRepository.SaveChanges())
                return NoContent();

            return BadRequest("Failed to Update Passenger");
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult> updatePassenger(PassengerUpdateDto passengerUpdateDto, int Id)
        {
            var passenger = await _passengerRepository.GetPassengerById(Id);
            if (passenger == null)
                return NotFound("Passenger Does not Exist");
            var user = await _userRepository.GetUserById((int)passenger.UserId!);
            if (passenger == null || user == null || passengerUpdateDto.User == null)
                return NotFound("User Does not Exist");
            if (passengerUpdateDto.User.Sex != null)
                passengerUpdateDto.User.Sex = passengerUpdateDto.User.Sex.ToUpper();
            if (user.Email != passengerUpdateDto.User.Email)
                if (await UserExists(passengerUpdateDto.User.Email))
                    return BadRequest("Email Duplicated");
                
            _passengerRepository.Update(passengerUpdateDto, passenger, user);

            if (await _passengerRepository.SaveChanges())
                return NoContent();

            return BadRequest("Failed to Update Passenger");
        }
        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PassengerDto>> GetPassengerById(int id)
        {
            var passenger = await _passengerRepository.GetPassengerDtoById(id);
            if (passenger == null)
                return NotFound("Passenger Does not Exist");

            return Ok(passenger);
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPost("AddPassenger")]
        public async Task<ActionResult> AddPassenger(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Email))
            {
                return BadRequest("Passenger Exists");
            }

            await _passengerRepository.CreatePassenger(registerDto);

            return StatusCode(201);
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN")]
        [HttpPut("updateRewardPoints")]
        public async Task<ActionResult> UpdateRewardPoints(int rp)
        {
            int Id = _tokenHandlerService.TokenHandler();

            var passenger = _passengerRepository.GetPassengerDtoById(Id);
            if (passenger == null)
                return NotFound("Passenger Does not Exist");

            await _passengerRepository.UpdateRewardPoints(rp, Id);

            if (await _passengerRepository.SaveChanges())
                return NoContent();

            return BadRequest("Failed to Update Reward Points");
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPut("updateRewardPoints/{Id}")]
        public async Task<ActionResult> UpdateRewardPoints(int rp, int Id)
        {
            var passenger = _passengerRepository.GetPassengerDtoById(Id);
            if (passenger == null)
                return NotFound("Passenger Does not Exist");

            await _passengerRepository.UpdateRewardPoints(rp, Id);

            if (await _passengerRepository.SaveChanges())
                return NoContent();

            return BadRequest("Failed to Update Reward Points");
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPut("updateRewardPointsToAll")]
        public async Task<ActionResult> UpdateRewardPointsToAll(int rp)
        {
            await _passengerRepository.UpdateRewardPointsToAll(rp);

            if (await _passengerRepository.SaveChanges())
                return NoContent();

            return BadRequest("Failed to Update Reward Points");
        }

        private async Task<bool> UserExists(string? Email)
        {
            return await _userRepository.GetUserByEmail(Email!) != null;
        }
    }
}