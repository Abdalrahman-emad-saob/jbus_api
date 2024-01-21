using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    public class DriverAccountController : BaseApiController
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IBlacklistedTokenRepository _blacklistedTokenRepository;

        public DriverAccountController(
            IDriverRepository driverRepository,
            IUserRepository userRepository,
            ITokenService tokenService,
            IMapper mapper,
            IBlacklistedTokenRepository blacklistedTokenRepository
            )
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _blacklistedTokenRepository = blacklistedTokenRepository;
        }

        [HttpPost("login")]
        public ActionResult<LoginDriverResponseDto> Login(LoginDto loginDto)
        {
            try
            {
                var user = _userRepository.GetUserByEmail(loginDto.Email!);

                if (user == null)
                    return NotFound(new { Error = "USER_DOES_NOT_EXIST" });

                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, loginDto.Password!);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                    return Unauthorized("Not Authorized1");

                if (user.Email == null)
                    return Unauthorized("Not Authorized2");

                var driverDto = _mapper.Map<DriverDto>(_driverRepository.GetDriverByEmail(user.Email));

                if (driverDto == null)
                    return Unauthorized("Not Authorized3");

                var token = _tokenService.CreateToken(user, driverDto.Id);

                return new LoginDriverResponseDto
                {
                    driverDto = driverDto,
                    Token = token
                };
            }
            catch (Exception)
            {
                return StatusCode(500, "Server Error");
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