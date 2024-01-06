using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [Authorize]
    public class AdminController : BaseApiController
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHandlerService _tokenHandlerService;

        public AdminController(
            IAdminRepository adminRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ITokenHandlerService tokenHandlerService)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenHandlerService = tokenHandlerService;
        }

        [HttpGet("getAdmins")]
        public ActionResult<IEnumerable<AdminDto>> GetAdmins() => Ok(_adminRepository.GetAdmins());

        [HttpGet("{id}")]
        public ActionResult<AdminDto> GetAdminById(int Id)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || (role.ToUpper() != "SUPER_ADMIN" && role.ToUpper() != "ADMIN"))
                return Unauthorized("Not authorized");

            return _adminRepository.GetAdminDtoById(Id);
        }
        [HttpPost("addAdmin")]
        public ActionResult addAdmin(RegisterAdminDto registerAdminDto)
        {
            if (UserExists(registerAdminDto.Email))
            {
                return BadRequest("Admin Exists");
            }
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || (role.ToUpper() != "SUPER_ADMIN" && role.ToUpper() != "ADMIN"))
                return Unauthorized("Not authorized");
            _adminRepository.CreateAdmin(registerAdminDto);
            return Created();
        }
        [HttpPut("{id}")]
        public ActionResult updateAdmin(AdminUpdateDto adminUpdateDto, int Id)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || (role.ToUpper() != "SUPER_ADMIN" && role.ToUpper() != "ADMIN"))
                return Unauthorized("Not authorized");

            var admin = _adminRepository.GetAdminById(Id);
            var user = _userRepository.GetUserById(admin.UserId!);

            if (admin == null || user == null)
                return NotFound();

            _mapper.Map(adminUpdateDto, admin);
            _mapper.Map(adminUpdateDto.User, user);
            _adminRepository.Update(Id);
            if (_adminRepository.SaveChanges())
                return NoContent();

            return BadRequest("Failed to Update Passenger");
        }

        [NonAction]
        public bool UserExists(string? Email)
        {
            return _userRepository.GetUserByEmail(Email!) != null;
        }
    }
}