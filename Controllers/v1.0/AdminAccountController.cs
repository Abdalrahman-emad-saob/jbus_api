using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    public class AdminAccountController(
        IAdminRepository adminRepository,
        IUserRepository userRepository,
        ITokenService tokenService,
        IMapper mapper,
        IBlacklistedTokenRepository blacklistedTokenRepository
            ) : BaseApiController
    {
        private readonly IAdminRepository _adminRepository = adminRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IMapper _mapper = mapper;
        private readonly IBlacklistedTokenRepository _blacklistedTokenRepository = blacklistedTokenRepository;

        [HttpPost("login")]
        public async Task<ActionResult<LoginAdminResponseDto>> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(loginDto.Email!);

                if (user == null)
                    return NotFound(new { Error = "USER_DOES_NOT_EXIST" });

                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, loginDto.Password!);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                    return Unauthorized("Not Authorized1");

                if (user.Email == null)
                    return Unauthorized("Not Authorized2");

                var adminDto = _mapper.Map<AdminDto>(await _adminRepository.GetAdminByEmail(user.Email));

                if (adminDto == null)
                    return Unauthorized("Not Authorized3");

                var token = _tokenService.CreateToken(user, adminDto.Id);

                adminDto.Role = user.Role.ToString();

                return new LoginAdminResponseDto
                {
                    adminDto = adminDto,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error {ex}");
            }
        }
        [Authorize]
        [HttpPost("logout")]
        public ActionResult logout()
        {
            var token = _tokenService.GetToken();
            _blacklistedTokenRepository.BlacklistTokenAsync(token);
            return Ok(new { Message = "Logged Out Successfully" });
        }

    }
}