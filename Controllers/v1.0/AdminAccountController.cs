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
        private readonly ITokenHandlerService _tokenHandlerService;
        private readonly IMapper _mapper;

        public AdminAccountController(
            DataContext context,
            IAdminRepository adminRepository,
            ITokenService tokenService,
            ITokenHandlerService tokenHandlerService,
            IMapper mapper)
        {
            _context = context;
            _adminRepository = adminRepository;
            _tokenService = tokenService;
            _tokenHandlerService = tokenHandlerService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPost("addAdmin")]
        public ActionResult<LoginAdminResponseDto> addAdmin(RegisterAdminDto registerDto)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || role.ToUpper() != "SUPER_ADMIN" || role.ToUpper() != "ADMIN")
                return Unauthorized("Not authorized");
    
            var user = new User
            {
                Role = Role.SUPER_ADMIN,
                Name = registerDto.Name,
                PhoneNumber = registerDto.PhoneNumber,
                Email = registerDto.Email?.ToLower()
            };
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, registerDto.Password!);
            var admin = new Admin()
            {
                User = user
            };
            var token = _tokenService.CreateToken(user, admin.Id);

            _context.Users.Add(user);
            _context.Admins.Add(admin);
            _adminRepository.SaveChanges();
            return new LoginAdminResponseDto
            {
                adminDto = _mapper.Map<AdminDto>(admin),
                Token = token
            };
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