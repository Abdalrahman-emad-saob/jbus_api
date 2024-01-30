using API.DTOs;
using API.Interfaces;
using API.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
    public class DriverController(
            IDriverRepository driverRepository,
            IUserRepository userRepository,
            IMapper mapper
                ) : BaseApiController
    {
        private readonly IDriverRepository _driverRepository = driverRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetDrivers()
        {
            return Ok(await _driverRepository.GetDrivers());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverDto?>> GetDriverById(int id)
        {
            var driverDto = await _driverRepository.GetDriverDtoById(id);
            if (driverDto == null)
                return NotFound(new { Error = "Driver Not Found" });
            return driverDto;
        }
        [HttpPost("addDriver")]
        public async Task<ActionResult<DriverDto>> CreateDriver(RegisterDriverDto registerDriverDto)
        {
            if (await UserExists(registerDriverDto.Email))
            {
                return BadRequest(new { Error = "Driver Exists" });
            }
            await _driverRepository.CreateDriver(registerDriverDto);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> updateDriver(int id, DriverUpdateDto driverUpdateDto)
        {
            var driver = await _driverRepository.GetDriverById(id);

            if (driver == null || driverUpdateDto.User == null)
                return NotFound(new { Error = "Driver Not Found" });

            var user = await _userRepository.GetUserById(driver.UserId);

            if (user == null)
                return NotFound(new { Error = "Driver Not Found" });

            if (driverUpdateDto.User.Sex != null)
                driverUpdateDto.User.Sex = driverUpdateDto.User.Sex.ToUpper();

            if (user.Email != driverUpdateDto.User.Email)
                if (await UserExists(driverUpdateDto.User.Email))
                    return BadRequest(new { Error = "Email Duplicated" });

            _mapper.Map(driverUpdateDto, driver);
            _mapper.Map(driverUpdateDto.User, user);

            if (await _driverRepository.SaveChanges())
                return NoContent();

            return BadRequest(new { Error = "Failed to Update Driver" });
        }
        private async Task<bool> UserExists(string? Email)
        {
            return await _userRepository.GetUserByEmail(Email!) != null;
        }

    }
}