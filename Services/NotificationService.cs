using FirebaseAdmin.Messaging;
using System.Threading.Tasks;

namespace API.Services;

public class NotificationService
{
    public async Task SendNotificationAsync(string deviceToken, string title, string body)
    {
        var message = new Message
        {
            Token = deviceToken,
            Notification = new Notification
            {
                Title = title,
                Body = body,
            },
        };

        await FirebaseMessaging.DefaultInstance.SendAsync(message);
    }
}
