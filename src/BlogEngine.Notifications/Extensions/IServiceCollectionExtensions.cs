
using BlogEngine.Notifications.Options;
using BlogEngine.Notifications.Senders;
using BlogEngine.Notifications.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<EmailOptions>(configuration)
                .AddScoped<INotificationSender, EmailSender>()
                .AddScoped<IViewRenderService, ViewRenderService>()
                .AddTransient<INotificationService, NotificationService>();
        }
    }
}
