using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Services;
using API.Validations;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class NotificationController(
    NotificationService notificationService,
    ITokenHandlerService tokenHandlerService,
    INotisTokenRepository notisTokenRepository
        ) : BaseApiController
{
    private readonly NotificationService _notificationService = notificationService;
    private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;
    private readonly INotisTokenRepository _notisTokenRepository = notisTokenRepository;

    [HttpPost("register")]
    public async Task<IActionResult> RegisterDeviceToken(string? deviceToken)
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized("Not authorized1");

        if (string.IsNullOrEmpty(deviceToken))
            return BadRequest(new { Error = "Device Token is Required" });

        bool registred = await _notisTokenRepository.StoreDeviceToken(Id, deviceToken);
        if (registred)
            return Ok(new { Success = "Device token registered successfully." });

        return StatusCode(500, new { Error = "Serve Error" });
    }
    [CustomAuthorize("Passenger")]
    [HttpPost("sendNoti")]
    public async Task<IActionResult> SendNotification(NotificationDto notificationDto)
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized(new { Error = "Not authorized1"});

        string? deviceToken = await GetDeviceTokenFromUserId(Id)!;
        if (deviceToken == null)
            return NotFound(new { Error = "FCM Token is not found" });

        await _notificationService.SendNotificationAsync(deviceToken, notificationDto);

        return Ok(new { Success = "Notification sent successfully." });
    }

    [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
    [HttpPost("sendNotisToAll")]
    public IActionResult sendNotisToAll(NotificationDto notificationDto)
    {
        BackgroundJob.Enqueue(() => _notificationService.SendNotificationsToAllAsync(notificationDto).Wait());

        return Ok(new { Success = "Notifications sent successfully." });
    }

    private async Task<string?> GetDeviceTokenFromUserId(int? PassengerId)
    {
        return await _notisTokenRepository.GetDeviceToken(PassengerId);
    }


}