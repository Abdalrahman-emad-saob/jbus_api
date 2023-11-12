using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<UserDto> Register(RegisterDto registerDto)
        {
            if (UserExists(registerDto.Email))
            {
                return BadRequest("User Exists");
            }
            using var hmac = new HMACSHA512();
            var user = new User
            {
                Name = registerDto.Name.ToLower(),
                PhoneNumber = registerDto.PhoneNumber,
                Email = registerDto.Email.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return new UserDto
            {
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<UserDto> Login(LoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == loginDto.Email.ToLower());
            if (user == null) return Unauthorized("Not Authorized");


            using var hmac = new HMACSHA512(user.PasswordSalt);
            byte[] ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < ComputedHash.Length; i++)
            {
                if (ComputedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Not Authorized");
            }

            return new UserDto
            {
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
        [HttpGet("UserExists")]
        public bool UserExists(string Email)
        {
            return _context.Users.Any(x => x.Email == Email.ToLower());
        }
    }
}