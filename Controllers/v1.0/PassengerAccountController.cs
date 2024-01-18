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
    public class PassengerAccountController : BaseApiController
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOTPRepository _oTPRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IBlacklistedTokenRepository _blacklistedTokenRepository;

        public PassengerAccountController(
            IPassengerRepository passengerRepository,
            IUserRepository userRepository,
            IOTPRepository oTPRepository,
            ITokenService tokenService,
            IMapper mapper,
            IBlacklistedTokenRepository blacklistedTokenRepository
            )
        {
            _passengerRepository = passengerRepository;
            _userRepository = userRepository;
            _oTPRepository = oTPRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _blacklistedTokenRepository = blacklistedTokenRepository;
        }
        [HttpPost("register/{OTP}")]
        public ActionResult<LoginResponseDto> Register(RegisterDto registerDto, int OTP)
        {
            OTP otp = _oTPRepository.GetOTPByEmail(registerDto.Email!);
            if (otp == null)
                return NotFound(new { Message = "OTP not found" });

            if (otp.Otp != OTP)
                return BadRequest("Invalid OTP");

            _oTPRepository.DeleteOTP(otp.Id);

            var CreatePassenger = _passengerRepository.CreatePassenger(registerDto);
            var user = CreatePassenger.user;
            var passengerDto = CreatePassenger.passengerDto;

            if (passengerDto == null || user == null)
                return StatusCode(500, "Internal Server Error inside");

            var token = _tokenService.CreateToken(user, passengerDto.Id);
            return StatusCode(201, new LoginResponseDto
            {
                passengerDto = passengerDto,
                Token = token
            });
        }
        [HttpPost("login")]
        public ActionResult<LoginResponseDto> Login(LoginDto loginDto)
        {
            try
            {
                if (loginDto.Email == null)
                    return BadRequest("Email is Empty");

                var user = _userRepository.GetUserByEmail(loginDto.Email);

                if (user == null || user.PasswordHash == null || loginDto.Password == null)
                    return Unauthorized("Not Authorized1");

                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                    return Unauthorized("Not Authorized2");

                var passengerDto = _passengerRepository.GetPassengerDtoByEmail(loginDto.Email);

                if (passengerDto == null)
                    return Unauthorized("Not Authorized3");

                var token = _tokenService.CreateToken(user, passengerDto.Id);

                return Ok(new LoginResponseDto
                {
                    passengerDto = passengerDto,
                    Token = token
                });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [NonAction]
        private bool UserExists(string? Email)
        {
            return _userRepository.GetUserByEmail(Email!) != null;
        }

        [HttpPost("sendOTP")]
        public IActionResult SendOtp(sendOTPDto sendOTPDto)
        {
            if (UserExists(sendOTPDto.Email))
            {
                return BadRequest("Passenger exists");
            }

            int otp = _oTPRepository.CreateOTP(sendOTPDto.Email!);
            return Ok(otp);
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