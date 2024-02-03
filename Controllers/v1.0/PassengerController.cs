using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Validations;

namespace API.Controllers.v1
{
    public class PassengerController(
        IPassengerRepository passengerRepository,
        IUserRepository userRepository,
        IFazaaRepository fazaaRepository,
        ITokenHandlerService tokenHandlerService
        ) : BaseApiController
    {
        private readonly IPassengerRepository _passengerRepository = passengerRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IFazaaRepository _fazaaRepository = fazaaRepository;
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
                return Unauthorized(new { Error = "Not authorized" });

            return await _passengerRepository.GetPassengerDtoById(Id);
        }

        [CustomAuthorize("PASSENGER")]
        [HttpPut]
        public async Task<ActionResult> updatePassenger(PassengerUpdateDto passengerUpdateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized(new { Error = "Not authorized" });

            var passenger = await _passengerRepository.GetPassengerById(Id);
            if (passenger == null)
                return NotFound(new { Error = "Passenger Does not Exist" });

            var user = await _userRepository.GetUserById((int)passenger.UserId!);

            if (passenger == null || user == null)
                return NotFound();

            _passengerRepository.Update(passengerUpdateDto, passenger, user);

            if (await _passengerRepository.SaveChanges())
                return NoContent();

            return BadRequest(new { Error = "Failed to Update Passenger" });
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult> updatePassenger(PassengerUpdateDto passengerUpdateDto, int Id)
        {
            var passenger = await _passengerRepository.GetPassengerById(Id);
            if (passenger == null)
                return NotFound(new { Error = "Passenger Does not Exist" });

            var user = await _userRepository.GetUserById((int)passenger.UserId!);
            if (passenger == null || user == null || passengerUpdateDto.User == null)
                return NotFound(new { Error = "User Does not Exist" });

            if (passengerUpdateDto.User.Sex != null)
                passengerUpdateDto.User.Sex = passengerUpdateDto.User.Sex.ToUpper();
                
            if (user.Email != passengerUpdateDto.User.Email)
                if (await UserExists(passengerUpdateDto.User.Email))
                    return BadRequest(new { Error = "Email Duplicated" });

            _passengerRepository.Update(passengerUpdateDto, passenger, user);

            if (await _passengerRepository.SaveChanges())
                return NoContent();

            return BadRequest(new { Error = "Failed to Update Passenger" });
        }
        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PassengerDto>> GetPassengerById(int id)
        {
            var passenger = await _passengerRepository.GetPassengerDtoById(id);
            if (passenger == null)
                return NotFound(new { Error = "Passenger Does not Exist" });

            return Ok(passenger);
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPost("AddPassenger")]
        public async Task<ActionResult> AddPassenger(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Email))
            {
                return BadRequest(new { Error = "Passenger Exists" });
            }

            await _passengerRepository.CreatePassenger(registerDto);
            if (await _passengerRepository.SaveChanges())
                return Created("Created", null);

            return StatusCode(500, new { Error = "Server Error" });
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN")]
        [HttpPut("updateRewardPoints")]
        public async Task<ActionResult> UpdateRewardPoints(UpdateRPsDto rp)
        {
            int Id = _tokenHandlerService.TokenHandler();

            var passenger = _passengerRepository.GetPassengerDtoById(Id);
            if (passenger == null)
                return NotFound(new { Error = "Passenger Does not Exist" });

            await _passengerRepository.UpdateRewardPoints(rp.rp, Id);

            if (await _passengerRepository.SaveChanges())
                return NoContent();

            return BadRequest(new { Error = "Failed to Update Reward Points" });
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPut("updateRewardPoints/{Id}")]
        public async Task<ActionResult> UpdateRewardPoints(UpdateRPsDto rp, int Id)
        {
            var passenger = await _passengerRepository.GetPassengerDtoById(Id);
            if (passenger == null)
                return NotFound(new { Error = "Passenger Does not Exist" });

            await _passengerRepository.UpdateRewardPoints(rp.rp, Id);

            if (await _passengerRepository.SaveChanges())
                return NoContent();

            return BadRequest(new { Error = "Failed to Update Reward Points" });
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPut("updateRewardPointsToAll")]
        public async Task<ActionResult> UpdateRewardPointsToAll(UpdateRPsDto rp)
        {
            await _passengerRepository.UpdateRewardPointsToAll(rp.rp);

            if (await _passengerRepository.SaveChanges())
                return NoContent();

            return BadRequest(new { Error = "Failed to Update Reward Points" });
        }

        [CustomAuthorize("PASSENGER")]
        [HttpGet("IsPassengerFaza'aable")]
        public async Task<ActionResult> IsPassengerFazaaable()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized(new { Error = "Not authorized" });

            var fazaa = await _fazaaRepository.GetFazaaByPassengerId(Id);

            if (fazaa == null)
                return Ok(new { Success = true });

            return BadRequest(new { Success = false });
        }

        private async Task<bool> UserExists(string? Email)
        {
            return await _userRepository.GetUserByEmail(Email!) != null;
        }
    }
}