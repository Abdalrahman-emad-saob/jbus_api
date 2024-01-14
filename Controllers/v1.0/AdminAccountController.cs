using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    public class AdminAccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AdminAccountController(
            DataContext context,
            IAdminRepository adminRepository,
            IUserRepository userRepository,
            ITokenService tokenService,
            IMapper mapper)
        {
            _context = context;
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        
        [HttpPost("login")]
        public ActionResult<LoginAdminResponseDto> Login(LoginDto loginDto)
        {
            try
            {
                var user = _userRepository.GetUserByEmail(loginDto.Email!);

                if (user == null)
                    return Unauthorized("Not Authorized1");

                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, loginDto.Password!);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                    return Unauthorized("Not Authorized2");

                if(user.Email == null)
                    return Unauthorized("Not Authorized3");

                var adminDto = _mapper.Map<AdminDto>(_adminRepository.GetAdminByEmail(user.Email));

                if (adminDto == null)
                    return Unauthorized("Not Authorized4");

                var token = _tokenService.CreateToken(user, adminDto.Id);

                return new LoginAdminResponseDto
                {
                    adminDto = adminDto,
                    Token = token
                };
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}