using API.DTOs;
using API.Interfaces;
using API.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
    public class DriverController : BaseApiController
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandlerService _tokenHandlerService;
        private readonly IMapper _mapper;

        public DriverController(
                IDriverRepository driverRepository,
                IUserRepository userRepository,
                ITokenHandlerService tokenHandlerService,
                IMapper mapper
                )
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _tokenHandlerService = tokenHandlerService;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<DriverDto>> GetDrivers()
        {
            return Ok(_driverRepository.GetDrivers());
        }
        [HttpGet("{id}")]
        public ActionResult<DriverDto> GetDriverById(int id)
        {
            var driverDto = _driverRepository.GetDriverDtoById(id);
            if (driverDto == null)
                return NotFound("Driver Not Found");
            return driverDto;
        }
        [HttpPost("addDriver")]
        public ActionResult<DriverDto> CreateDriver(RegisterDriverDto registerDriverDto)
        {
            if (UserExists(registerDriverDto.Email))
            {
                return BadRequest("Driver Exists");
            }
            _driverRepository.CreateDriver(registerDriverDto);
            return Ok();
        }
        [HttpPut("{id}")]
        public ActionResult updateDriver(int id, DriverUpdateDto driverUpdateDto)
        {
            var driver = _driverRepository.GetDriverById(id);

            if (driver == null || driverUpdateDto.User == null)
                return NotFound("Driver Not Found");

            var user = _userRepository.GetUserById(driver.UserId);

            if (user == null)
                return NotFound("Driver Not Found");

            if (driverUpdateDto.User.Sex != null)
                driverUpdateDto.User.Sex = driverUpdateDto.User.Sex.ToUpper();

            if (user.Email != driverUpdateDto.User.Email)
                if (UserExists(driverUpdateDto.User.Email))
                    return BadRequest("Email Duplicated");

            _mapper.Map(driverUpdateDto, driver);
            _mapper.Map(driverUpdateDto.User, user);

            if (_driverRepository.SaveChanges())
                return NoContent();

            return BadRequest("Failed to Update Driver");
        }
        private bool UserExists(string? Email)
        {
            return _userRepository.GetUserByEmail(Email!) != null;
        }

    }
}