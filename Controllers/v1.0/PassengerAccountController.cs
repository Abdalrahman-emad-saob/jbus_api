using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    public class PassengerAccountController : BaseApiController
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IOTPRepository _oTPRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public PassengerAccountController(
            IPassengerRepository passengerRepository,
            IOTPRepository oTPRepository,
            ITokenService tokenService,
            IMapper mapper)
        {
            _passengerRepository = passengerRepository;
            _oTPRepository = oTPRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("register")]
        public ActionResult<LoginResponseDto> Register(RegisterDto registerDto, int OTP)
        {
            OTP otp = _oTPRepository.GetOTPByEmail(registerDto.Email!);
            if (otp.Otp == OTP)
                return BadRequest("Invalid OTP");
            _oTPRepository.DeleteOTP(otp.Id);
            _passengerRepository.CreatePassenger(registerDto);

            return Ok(_passengerRepository.CreatePassenger(registerDto));
        }
        [HttpPost("login")]
        public ActionResult<LoginResponseDto> Login(LoginDto loginDto)
        {
            try
            {
                if(loginDto.Email == null)
                    return BadRequest("Emai is Empty");

                var user = _passengerRepository.GetUserByEmail(loginDto.Email);

                if (user == null)
                    return Unauthorized("Not Authorized");

                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, loginDto.Password!);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                    return Unauthorized("Not Authorized");

                var passengerDto = _mapper.Map<PassengerDto>(_passengerRepository.GetPassengerById(user.Id));

                if (passengerDto == null)
                    return Unauthorized("Not Authorized");

                var token = _tokenService.CreateToken(user, passengerDto.Id);

                return new LoginResponseDto
                {
                    passengerDto = passengerDto,
                    Token = token
                };
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [NonAction]
        public bool PassengerExists(string? Email)
        {
            return _passengerRepository.GetPassengerByEmail(Email) != null;
        }

        [HttpPost("sendOTP")]
        public ActionResult SendOTP(string Email)
        {
            if (PassengerExists(Email))
            {
                return BadRequest("Passenger Exists");
            }
            int otp = _oTPRepository.CreateOTP(Email);
            return Ok(otp);
        }

    }
}