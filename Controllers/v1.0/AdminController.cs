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

        [HttpGet]
        public ActionResult<AdminDto> GetAdminById()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || (role.ToUpper() != "SUPER_ADMIN" && role.ToUpper() != "ADMIN"))
                return Unauthorized("Not authorized");

            return _adminRepository.GetAdminDtoById(Id);
        }
        [Authorize]
        [HttpPost("addAdmin")]
        public ActionResult<AdminDto> addAdmin(RegisterAdminDto registerAdminDto)
        {
            if (UserExists(registerAdminDto.Email))
            {
                return BadRequest("Admin Exists");
            }
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || (role.ToUpper() != "SUPER_ADMIN" && role.ToUpper() != "ADMIN"))
                return Unauthorized("Not authorized");

            return _adminRepository.CreateAdmin(registerAdminDto);
        }
        [HttpPut]
        public ActionResult updateAdmin(AdminUpdateDto adminUpdateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || (role.ToUpper() != "SUPER_ADMIN" && role.ToUpper() != "ADMIN"))
                return Unauthorized("Not authorized");

            var admin = _adminRepository.GetAdminById(Id);
            var user = _userRepository.GetUserById((int)admin.UserId!);

            if (admin == null || user == null) return NotFound();
            _mapper.Map(adminUpdateDto, admin);
            _mapper.Map(adminUpdateDto.User, user);

            if (_adminRepository.SaveChanges()) return NoContent();
            return BadRequest("Failed to Update Passenger");
        }
        [NonAction]
        public bool UserExists(string? Email)
        {
            return _userRepository.GetUserByEmail(Email!) != null;
        }
    }
}