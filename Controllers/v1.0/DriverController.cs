using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [Authorize]
    public class DriverController : BaseApiController
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandlerService _tokenHandlerService;
        private readonly IMapper _mapper;

        public DriverController(IDriverRepository driverRepository,
                                IUserRepository userRepository,
                                ITokenHandlerService tokenHandlerService,
                                IMapper mapper)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _tokenHandlerService = tokenHandlerService;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<DriverDto>> GetDrivers()
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || role.ToUpper() != "SUPER_ADMIN" || role.ToUpper() != "ADMIN")
                return Unauthorized("Not authorized");
    
            return Ok(_driverRepository.GetDrivers());
        }
        [HttpGet("{id}")]
        public ActionResult<DriverDto> GetDriverById(int id)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || role.ToUpper() != "SUPER_ADMIN" || role.ToUpper() != "ADMIN")
                return Unauthorized("Not authorized");
    
            return _driverRepository.GetDriverDtoById(id);
        }
        [HttpPost("addDriver")]
        public ActionResult<DriverDto> CreateDriver(DriverCreateDto driverDto)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || role.ToUpper() != "SUPER_ADMIN" || role.ToUpper() != "ADMIN")
                return Unauthorized("Not authorized");
    
            if(DriverExists(driverDto.Email))
            {
                return BadRequest("Driver Exists");
            }
            _driverRepository.CreateDriver(driverDto);
            return Ok();
        }
        [HttpPut("{id}")]
        public ActionResult updateDriver(int id, DriverUpdateDto driverUpdateDto)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || role.ToUpper() != "SUPER_ADMIN" || role.ToUpper() != "ADMIN")
                return Unauthorized("Not authorized");
    
            var driver = _driverRepository.GetDriverById(id);
            var user = _userRepository.GetUserById((int)driver.UserId!);

            if (driver == null) return NotFound();
            _mapper.Map(driverUpdateDto, driver);
            _mapper.Map(driverUpdateDto.User, user);
            if (_driverRepository.SaveChanges()) return NoContent();

            return BadRequest("Failed to Update Driver");
        }
        [NonAction]
        public bool DriverExists(string? Email)
        {
            return _driverRepository.GetDriverByEmail(Email!) != null;
        }
    }
}