using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
    public class AdminController(
        IAdminRepository adminRepository,
        IUserRepository userRepository,
        IMapper mapper
            ) : BaseApiController
    {
        private readonly IAdminRepository _adminRepository = adminRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet("getAdmins")]
        public async Task<ActionResult<IEnumerable<AdminDto>>> GetAdmins()
        {
            return Ok(await _adminRepository.GetAdmins());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminDto>> GetAdminById(int Id)
        {
            var admin = await _adminRepository.GetAdminDtoById(Id);

            if (admin == null)
                return NotFound(new { Error = "Admin Not Found" });

            return admin;
        }
        [HttpPost("addAdmin")]
        public ActionResult addAdmin(RegisterAdminDto registerAdminDto)
        {
            if (UserExists(registerAdminDto.Email))
                return BadRequest(new { Error = "Admin Exists" });

            _adminRepository.CreateAdmin(registerAdminDto);

            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> updateAdmin(AdminUpdateDto adminUpdateDto, int Id)
        {
            var admin = await _adminRepository.GetAdminById(Id);


            if (admin == null || adminUpdateDto.User == null)
                return NotFound(new { Error = "Admin Not Found" });

            var user = await _userRepository.GetUserById(admin.UserId!);
            if (user == null)
                return NotFound(new { Error = "User Not Found" });

            if (adminUpdateDto.User.Sex != null)
                adminUpdateDto.User.Sex = adminUpdateDto.User.Sex.ToUpper();

            if (user.Email != adminUpdateDto.User.Email)
                if (UserExists(adminUpdateDto.User.Email))
                    return BadRequest(new { Error = "Email Duplicated" });

            _mapper.Map(adminUpdateDto, admin);
            _mapper.Map(adminUpdateDto.User, user);
            await _adminRepository.Update(Id);

            if (await _adminRepository.SaveChanges())
                return NoContent();

            return BadRequest(new { Error = "Failed to Update Passenger" });
        }

        private bool UserExists(string? Email)
        {
            return _userRepository.GetUserByEmail(Email!) != null;
        }
    }
}