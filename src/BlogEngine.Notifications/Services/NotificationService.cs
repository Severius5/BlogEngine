using BlogEngine.Notifications.Models;
using BlogEngine.Notifications.Models.Templates;
using BlogEngine.Notifications.Senders;
using System.Threading.Tasks;

namespace BlogEngine.Notifications.Services
{
    internal class NotificationService : INotificationService
    {
        private readonly INotificationSender _sender;
        private readonly IViewRenderService _viewRender;

        public NotificationService(INotificationSender sender, IViewRenderService viewRender)
        {
            _sender = sender ?? throw new System.ArgumentNullException(nameof(sender));
            _viewRender = viewRender ?? throw new System.ArgumentNullException(nameof(viewRender));
        }

        public async Task SendNotification(EmailTemplate template)
        {
            if (template.ViewModel != null)
                template.ViewModel.BlogHost = "todo";

            var body = await _viewRender.RenderToString(template.ViewName, template.ViewModel);
            var notification = new Notification
            {
                Body = body,
                Recipients = template.Recipients,
                Subject = template.Subject
            };

            await _sender.Send(notification);
        }
    }
}
