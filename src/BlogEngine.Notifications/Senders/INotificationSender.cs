using BlogEngine.Notifications.Models;
using System.Threading.Tasks;

namespace BlogEngine.Notifications.Senders
{
    internal interface INotificationSender
    {
        Task<bool> Send(Notification notification);
    }
}
