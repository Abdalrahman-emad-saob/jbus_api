using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Services;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class NotificationController : BaseApiController
    {
        private readonly NotificationService _notificationService;
        private readonly ITokenHandlerService _tokenHandlerService;
        private readonly INotisTokenRepository _notisTokenRepository;

        public NotificationController(
            NotificationService notificationService,
            ITokenHandlerService tokenHandlerService,
            INotisTokenRepository notisTokenRepository
            )
        {
            _notificationService = notificationService;
            _tokenHandlerService = tokenHandlerService;
            _notisTokenRepository = notisTokenRepository;
        }

        [HttpPost("register")]
        public IActionResult RegisterDeviceToken(string? deviceToken)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            if (string.IsNullOrEmpty(deviceToken))
                return BadRequest(new { Message = "Device Token is Required" });

            bool registred = _notisTokenRepository.StoreDeviceToken(Id, deviceToken);
            if (registred)
                return Ok(new { Message = "Device token registered successfully." });

            return StatusCode(500, new { Message = "Serve Error" });
        }

        [HttpPost("sendNoti")]
        public async Task<IActionResult> SendNotification(NotificationDto notificationDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            string deviceToken = GetDeviceTokenFromUserId(Id)!;
            if(deviceToken == null)
                return NotFound(new { Message = "FCM Token is not found"});

            await _notificationService.SendNotificationAsync(deviceToken, notificationDto.Title!, notificationDto.Body!);

            return Ok(new { Message = "Notification sent successfully." });
        }

        private string? GetDeviceTokenFromUserId(int? PassengerId)
        {
            return _notisTokenRepository.GetDeviceToken(PassengerId);
        }
    }
}