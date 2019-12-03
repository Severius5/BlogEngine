using BlogEngine.Notifications.Models.Templates;
using System.Threading.Tasks;

namespace BlogEngine.Notifications.Services
{
    public interface INotificationService
    {
        Task SendNotification(EmailTemplate template);
    }
}
