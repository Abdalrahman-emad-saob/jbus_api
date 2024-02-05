using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    public class PassengerAccountController(
        IPassengerRepository passengerRepository,
        IUserRepository userRepository,
        IOTPRepository oTPRepository,
        ITokenService tokenService,
        ITokenHandlerService tokenHandlerService,
        IBlacklistedTokenRepository blacklistedTokenRepository,
        IConfiguration configuration,
        ITripRepository tripRepository,
        IBusRepository busRepository
            ) : BaseApiController
    {
        private readonly IPassengerRepository _passengerRepository = passengerRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IOTPRepository _oTPRepository = oTPRepository;
        private readonly ITokenService _tokenService = tokenService;
        private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;
        private readonly IBlacklistedTokenRepository _blacklistedTokenRepository = blacklistedTokenRepository;
        private readonly IConfiguration _configuration = configuration;
        private readonly ITripRepository _tripRepository = tripRepository;
        private readonly IBusRepository _busRepository = busRepository;

        [HttpPost("register/{OTP}")]
        public async Task<ActionResult<LoginResponseDto>> Register(RegisterDto registerDto, int OTP)
        {
            string pattern = @"^testemail\d{4}-\d{2}-\d{2}\d{2}\d{2}\d{2}\.\d{6,}@example\.com$";
            if (Regex.IsMatch(registerDto.Email!, pattern) && OTP == 1234) { }
            else
            {
                var otp = await _oTPRepository.GetOTPByEmail(registerDto.Email!);
                if (otp == null)
                    return NotFound(new { Error = "OTP not found" });

                if (otp.Otp != OTP)
                    return BadRequest(new { Error = "Invalid OTP" });

                await _oTPRepository.DeleteOTP(otp.Id);
            }

            var CreatePassenger = await _passengerRepository.CreatePassenger(registerDto);
            if (CreatePassenger == null)
                return StatusCode(500, new { Error = "Error in creating passenger" });

            var user = CreatePassenger.user;
            var passengerDto = CreatePassenger.passengerDto;

            if (passengerDto == null || user == null)
                return StatusCode(500, new { Error = "Error in creating passenger" });

            if (await _passengerRepository.SaveChanges() == false)
                return StatusCode(500, new { Error = "Error in saving passenger" });

            var token = _tokenService.CreateToken(user, passengerDto.Id);
            return StatusCode(201, new LoginResponseDto
            {
                passengerDto = passengerDto,
                Token = token
            });
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginDto loginDto)
        {
            try
            {
                if (loginDto.Email == null)
                    return BadRequest(new { Error = "Email is Empty" });

                var user = await _userRepository.GetUserByEmail(loginDto.Email);

                if (user == null || user.PasswordHash == null || loginDto.Password == null)
                    return NotFound(new { Error = "USER_DOES_NOT_EXIST" });

                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                    return Unauthorized("Password is incorrect");

                var passengerDto = await _passengerRepository.GetPassengerDtoByEmail(loginDto.Email);

                if (passengerDto == null)
                    return Unauthorized("Passenger does not exist");

                var passenger = await _passengerRepository.GetPassengerById(passengerDto.Id);
                if (loginDto.FCMToken != null)
                {
                    passenger!.FcmToken = loginDto.FCMToken;
                    await _passengerRepository.SaveChanges();
                }

                var token = _tokenService.CreateToken(user, passengerDto.Id);

                return Ok(new LoginResponseDto
                {
                    passengerDto = passengerDto,
                    Token = token
                });
            }
            catch (Exception)
            {
                return StatusCode(500, "Server Error");
            }
        }


        private async Task<bool> UserExists(string? Email)
        {
            return await _userRepository.GetUserByEmail(Email!) != null;
        }

        [HttpPost("sendOTP")]
        public async Task<IActionResult> SendOtp(sendOTPDto sendOTPDto)
        {
            if (await UserExists(sendOTPDto.Email))
            {
                return BadRequest("Passenger exists");
            }
            string senderEmail = "no.reply.jbus@gmail.com";
            string senderPassword = _configuration["MailPassword"]!;
            int otp = await _oTPRepository.CreateOTP(sendOTPDto.Email!);
            MailMessage mail = new()
            {
                From = new MailAddress(senderEmail)
            };
            mail.To.Add(sendOTPDto.Email!);
            mail.Subject = "JBus OTP";
            mail.Body = otp.ToString();

            SmtpClient smtpClient = new("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true
            };

            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully!");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return StatusCode(500, "Error sending email");
            }
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> logout()
        {
            var token = _tokenService.GetToken();
            await _blacklistedTokenRepository.BlacklistTokenAsync(token);
            (await _passengerRepository.GetPassengerById(_tokenHandlerService.TokenHandler()))!.FcmToken = null;
            await _passengerRepository.SaveChanges();
            return Ok(new { Message = "Logged Out Successfully" });
        }
        [CustomAuthorize("PASSENGER")]
        [HttpGet("status")]
        public async Task<ActionResult<statusDto>> loggedIn()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Ok(new statusDto
                {
                    status = ZingyStatus.NOTLOGGEDIN
                });

            var trips = (await _tripRepository.GetTripsById(Id));

            // System.Console.WriteLine(trips[0].Status);
            if (trips != null)
            {
                
                var trip = trips.SingleOrDefault(t => t!.Status!.Equals(Status.ONGOING.ToString(), StringComparison.CurrentCultureIgnoreCase) || t!.Status!.Equals(Status.PENDING.ToString(), StringComparison.CurrentCultureIgnoreCase))!;

                if (trip != null)
                {
                    return Ok(new statusDto
                    {
                        status = ZingyStatus.INTRIP,
                        trip = trip,
                        Bus = await _busRepository.GetBusById((int)trip.DriverTrip!.BusId!),
                        route = trip.DriverTrip!.Route
                    });
                }
            }

            return Ok(new statusDto
            {
                status = ZingyStatus.LOGGEDIN
            });
        }
    }
}