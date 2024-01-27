using API.DTOs;
using API.Interfaces;
using FirebaseAdmin.Messaging;

namespace API.Services;

public class NotificationService(
    INotisTokenRepository notisTokenRepository
    )
{
    private readonly INotisTokenRepository _notisTokenRepository = notisTokenRepository;

    public async Task SendNotificationAsync(string deviceToken, NotificationDto notificationDto)
    {
        var message = new Message
        {
            Token = deviceToken,
            Notification = new Notification
            {
                Title = notificationDto.Title,
                Body = notificationDto.Body,
            }
        };

        if (!string.IsNullOrEmpty(notificationDto.Type) && !string.IsNullOrEmpty(notificationDto.Value))
        {
            message.Data = new Dictionary<string, string>
        {
            { "type", notificationDto.Type },
            { "value", notificationDto.Value}
        };
        }
        string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        Console.WriteLine("Successfully sent message: " + response);
    }

    public async Task<bool> SendNotificationsToAllAsync(NotificationDto notificationDto)
    {
        List<string?> deviceTokens = await GetDeviceTokensForAllPassengers();

        if (deviceTokens == null || deviceTokens.Count == 0)
            return false;

        foreach (string? deviceToken in deviceTokens)
        {
            await SendNotificationAsync(deviceToken!, notificationDto);
        }

        return true;
    }

    private async Task<List<string?>> GetDeviceTokensForAllPassengers()
    {
        return await _notisTokenRepository.GetDeviceTokens();
    }
}
