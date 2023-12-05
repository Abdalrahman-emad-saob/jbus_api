using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    public class DriverController : BaseApiController
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DriverController(IDriverRepository driverRepository, IUserRepository userRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
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
            return _driverRepository.GetDriverDtoById(id);
        }
        [HttpPut("{id}")]
        public ActionResult updateDriver(int id, DriverUpdateDto driverUpdateDto)
        {
            var driver = _driverRepository.GetDriverById(id);
            var user = _userRepository.GetUserById(driver.UserId);

            if (driver == null) return NotFound();
            _mapper.Map(driverUpdateDto, driver);
            _mapper.Map(driverUpdateDto.User, user);
            if (_driverRepository.SaveChanges()) return NoContent();

            return BadRequest("Failed to Update Driver");
        }
    }
}