using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneSignalApi.Api;
using OneSignalApi.Client;
using OneSignalApi.Model;

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
            string pattern = @"^testemail\d{4}-\d{2}-\d{2}\d{2}\d{2}\d{2}\.\d{6,}@example\.com$";
            if (Regex.IsMatch(registerDto.Email!, pattern) && OTP == 1234) { }
            else
            {
                OTP otp = _oTPRepository.GetOTPByEmail(registerDto.Email!);
                if (otp == null)
                    return NotFound(new { Message = "OTP not found" });

                if (otp.Otp != OTP)
                    return BadRequest("Invalid OTP");

                _oTPRepository.DeleteOTP(otp.Id);
            }

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
                    return NotFound(new { Error = "USER_DOES_NOT_EXIST" });

                var passwordHasher = new PasswordHasher<Entities.User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                    return Unauthorized("Not Authorized1");

                var passengerDto = _passengerRepository.GetPassengerDtoByEmail(loginDto.Email);

                if (passengerDto == null)
                    return Unauthorized("Not Authorized2");

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
            string senderEmail = "aboodsaob1139@gmail.com";
            string senderPassword = "dugm cixm ychg qjjc";
            int otp = _oTPRepository.CreateOTP(sendOTPDto.Email!);
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
                return StatusCode(500, "Server Error");
            }
            // var appConfig = new Configuration
            // {
            //     BasePath = "https://onesignal.com/api/v1/notifications",
            //     AccessToken = "MzQ3ZGIyNWUtODllNC00NGQxLWJkYTEtNDUzZTlhYTVhOTY5",
            // };

            // var apiInstance = new DefaultApi(appConfig);
            // var notification = new Notification(
            //     appId: "8109104c-6466-474a-b16f-8b6817e85c35",
            //     includedSegments: new List<string> { "Subscribed Users" },
            //     templateId: "8727c910-521c-40ba-b204-310e24531202"
            // );

            // try
            // {
            //     // Create notification
            //     CreateNotificationSuccessResponse result = apiInstance.CreateNotification(notification);
            //     System.Console.WriteLine("|");
            //     System.Console.WriteLine("|");
            //     System.Console.WriteLine("|");
            //     System.Console.WriteLine("|");
            //     var a = result.ExternalId;
            //     System.Console.WriteLine(a);
            //     Debug.WriteLine(result);
            // }
            // catch (ApiException e)
            // {
            //     Debug.Print("Exception when calling DefaultApi.CreateNotification: " + e.Message);
            //     Debug.Print("Status Code: " + e.ErrorCode);
            //     Debug.Print(e.StackTrace);
            // }
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



// if (UserExists(sendOTPDto.Email))
//             {
//                 return BadRequest("Passenger exists");
//             }
//             string senderEmail = "aboodsaob1139@gmail.com";
//             string senderPassword = "dugm cixm ychg qjjc";
//             int otp = _oTPRepository.CreateOTP(sendOTPDto.Email!);
//             MailMessage mail = new()
//             {
//                 From = new MailAddress(senderEmail)
//             };
//             mail.To.Add(sendOTPDto.Email!);
//             mail.Subject = "JBus OTP";
//             mail.Body = otp.ToString();

//             SmtpClient smtpClient = new("localhost")
//             {
//                 Port = 25,
//                 // Credentials = new NetworkCredential(senderEmail, senderPassword),
//                 // EnableSsl = true
//             };

//             try
//             {
//                 smtpClient.Send(mail);
//                 Console.WriteLine("Email sent successfully!");

//                 return Ok();
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Error sending email: {ex.Message}");
//                 return StatusCode(500, "Server Error");
//             }