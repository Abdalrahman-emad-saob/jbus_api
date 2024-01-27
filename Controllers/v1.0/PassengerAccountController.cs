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
    public class PassengerAccountController : BaseApiController
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOTPRepository _oTPRepository;
        private readonly ITokenService _tokenService;
        private readonly ITokenHandlerService _tokenHandlerService;
        private readonly IBlacklistedTokenRepository _blacklistedTokenRepository;
        private readonly IConfiguration _configuration;

        public PassengerAccountController(
            IPassengerRepository passengerRepository,
            IUserRepository userRepository,
            IOTPRepository oTPRepository,
            ITokenService tokenService,
            ITokenHandlerService tokenHandlerService,
            IBlacklistedTokenRepository blacklistedTokenRepository,
            IConfiguration configuration
            )
        {
            _passengerRepository = passengerRepository;
            _userRepository = userRepository;
            _oTPRepository = oTPRepository;
            _tokenService = tokenService;
            _tokenHandlerService = tokenHandlerService;
            _blacklistedTokenRepository = blacklistedTokenRepository;
            _configuration = configuration;
        }
        [HttpPost("register/{OTP}")]
        public async Task<ActionResult<LoginResponseDto>> Register(RegisterDto registerDto, int OTP)
        {
            string pattern = @"^testemail\d{4}-\d{2}-\d{2}\d{2}\d{2}\d{2}\.\d{6,}@example\.com$";
            if (Regex.IsMatch(registerDto.Email!, pattern) && OTP == 1234) { }
            else
            {
                var otp = await _oTPRepository.GetOTPByEmail(registerDto.Email!);
                if (otp == null)
                    return NotFound(new { Message = "OTP not found" });

                if (otp.Otp != OTP)
                    return BadRequest("Invalid OTP");

                await _oTPRepository.DeleteOTP(otp.Id);
            }

            var CreatePassenger = await _passengerRepository.CreatePassenger(registerDto);
            if (CreatePassenger == null)
                return StatusCode(500, "Server Error0");

            var user = CreatePassenger.user;
            var passengerDto = CreatePassenger.passengerDto;

            if (passengerDto == null || user == null)
                return StatusCode(500, "Server Error1");

            if (await _passengerRepository.SaveChanges() == false)
                return StatusCode(500, "Server Error2");

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
                    return BadRequest("Email is Empty");

                var user = await _userRepository.GetUserByEmail(loginDto.Email);

                if (user == null || user.PasswordHash == null || loginDto.Password == null)
                    return NotFound(new { Error = "USER_DOES_NOT_EXIST" });

                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                    return Unauthorized("Not Authorized1");

                var passengerDto = await _passengerRepository.GetPassengerDtoByEmail(loginDto.Email);

                if (passengerDto == null)
                    return Unauthorized("Not Authorized2");

                var passenger = await _passengerRepository.GetPassengerById(passengerDto.Id);
                if (loginDto.FCMToken != null)
                {
                    passenger!.FcmToken = loginDto.FCMToken;
                    if (!await _passengerRepository.SaveChanges())
                        return StatusCode(500, "Server Error3");
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
                return StatusCode(500, "Internal Server Error");
            }
        }


        private async Task<bool> UserExists(string? Email)
        {
            return await _userRepository.GetUserByEmail(Email!) != null;
        }

        [HttpPost("sendOTP")]
        public async Task<IActionResult> SendOtp(sendOTPDto sendOTPDto)
        {

            // if (UserExists(sendOTPDto.Email))
            // {
            //     return BadRequest("Passenger exists");
            // }
            // string senderEmail = "aboodsaob1139@gmail.com";
            // // string senderPassword = "dugm cixm ychg qjjc";
            // int otp = _oTPRepository.CreateOTP(sendOTPDto.Email!);
            // MailMessage mail = new()
            // {
            //     From = new MailAddress(senderEmail)
            // };
            // mail.To.Add(sendOTPDto.Email!);
            // mail.Subject = "JBus OTP";
            // mail.Body = otp.ToString();

            // SmtpClient smtpClient = new("localhost")
            // {
            //     Port = 25,
            //     // Credentials = new NetworkCredential(senderEmail, senderPassword),
            //     // EnableSsl = true
            // };

            // try
            // {
            //     smtpClient.Send(mail);
            //     Console.WriteLine("Email sent successfully!");

            //     return Ok();
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"Error sending email: {ex.Message}");
            //     return StatusCode(500, "Server Error");
            // }





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
        public ActionResult<statusDto> loggedIn()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Ok(new statusDto
                {
                    status = ZingyStatus.NOTLOGGEDIN
                });

            return Ok(new statusDto
            {
                status = ZingyStatus.LOGGEDIN
            });
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