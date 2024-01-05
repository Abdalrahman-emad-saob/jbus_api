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
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AdminAccountController(
            DataContext context,
            IAdminRepository adminRepository,
            ITokenService tokenService,
            IMapper mapper)
        {
            _context = context;
            _adminRepository = adminRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        
        [HttpPost("login")]
        public ActionResult<LoginAdminResponseDto> Login(LoginDto loginDto)
        {
            try
            {
                var user = _context.Users.AsEnumerable()
                    .FirstOrDefault(x => x.Email != null && x.Email.Equals(loginDto.Email, StringComparison.CurrentCultureIgnoreCase));

                if (user == null)
                    return Unauthorized("Not Authorized");

                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, loginDto.Password!);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                    return Unauthorized("Not Authorized");

                var adminDto = _mapper.Map<AdminDto>(_adminRepository.GetAdminById(user.Id));

                if (adminDto == null)
                    return Unauthorized("Not Authorized");

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