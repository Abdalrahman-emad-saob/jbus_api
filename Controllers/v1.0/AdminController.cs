using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
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
        public ActionResult<IEnumerable<AdminDto>> GetAdmins()
        {
            return Ok(_adminRepository.GetAdmins());
        }

        [HttpGet("{id}")]
        public ActionResult<AdminDto> GetAdminById(int Id)
        {
            var admin = _adminRepository.GetAdminDtoById(Id);

            if (admin == null)
                return NotFound("Admin Not Found");

            return admin;
        }
        [HttpPost("addAdmin")]
        public ActionResult addAdmin(RegisterAdminDto registerAdminDto)
        {
            if (UserExists(registerAdminDto.Email))
                return BadRequest("Admin Exists");

            _adminRepository.CreateAdmin(registerAdminDto);

            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public ActionResult updateAdmin(AdminUpdateDto adminUpdateDto, int Id)
        {
            var admin = _adminRepository.GetAdminById(Id);


            if (admin == null || adminUpdateDto.User == null)
                return NotFound("Admin Not Found");

            var user = _userRepository.GetUserById(admin.UserId!);
            if (user == null)
                return NotFound("User Not Found");

            if (adminUpdateDto.User.Sex != null)
                adminUpdateDto.User.Sex = adminUpdateDto.User.Sex.ToUpper();

            if (user.Email != adminUpdateDto.User.Email)
                if (UserExists(adminUpdateDto.User.Email))
                    return BadRequest("Email Duplicated");

            _mapper.Map(adminUpdateDto, admin);
            _mapper.Map(adminUpdateDto.User, user);
            _adminRepository.Update(Id);

            if (_adminRepository.SaveChanges())
                return NoContent();

            return BadRequest("Failed to Update Passenger");
        }

        [NonAction]
        private bool UserExists(string? Email)
        {
            return _userRepository.GetUserByEmail(Email!) != null;
        }
    }
}