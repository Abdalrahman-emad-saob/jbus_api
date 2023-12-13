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
    public class AccountController : BaseApiController
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IOTPRepository _oTPRepository;
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(IPassengerRepository passengerRepository, IOTPRepository oTPRepository, DataContext context, ITokenService tokenService, IMapper mapper)
        {
            _passengerRepository = passengerRepository;
            _oTPRepository = oTPRepository;
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("register")]
        public ActionResult<PassengerDto> Register(RegisterDto registerDto)
        {
            OTP otp = _oTPRepository.GetOTPByEmail(registerDto.Email);
            if(otp.Otp == registerDto.OTP)
                return BadRequest("Invalid OTP");
            _oTPRepository.DeleteOTP(otp.Id);
            var user = new User
            {
                Name = registerDto.Name,
                PhoneNumber = registerDto.PhoneNumber,
                Email = registerDto.Email?.ToLower()
            };
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, registerDto.Password!);
            var passenger = new Passenger()
            {
                Wallet = 0,
                User = user
            };

            _context.Users.Add(user);
            _context.Passengers.Add(passenger);
            _passengerRepository.SaveChanges();
            return _mapper.Map<PassengerDto>(passenger);
        }
        [HttpPost("login")]
        public ActionResult<LoginResponseDto> Login(LoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Equals != null && x.Email!.Equals(loginDto.Email, StringComparison.CurrentCultureIgnoreCase));
            if (user == null) return Unauthorized("Not Authorized");


            var passwordHasher = new PasswordHasher<User>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, loginDto.Password!);

            if (passwordVerificationResult != PasswordVerificationResult.Success)
                return Unauthorized("Not Authorized");

            var passengerDto = _mapper.Map<PassengerDto>(_passengerRepository.GetPassengerById(user.Id));
            var token = _tokenService.CreateToken(user);

            return new LoginResponseDto
            {
                passengerDto = passengerDto,
                Token = token
            };
        }
        [NonAction]
        public bool PassengerExists(string? Email)
        {
            return _passengerRepository.GetPassengerByEmail(Email) != null;
        }

        [HttpPost("SendOTP")]
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