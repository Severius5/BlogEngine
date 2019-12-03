using BlogEngine.Core.Services;
using BlogEngine.Core.Services.Internals;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogCore(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddBlogStorage(configuration.GetSection("Storage"))
                .AddNotifications(configuration.GetSection("Notifications"));

            return services
                .AddTransient<IAuthService, AuthService>()
                .AddTransient<IPostsService, PostsService>()
                .AddTransient<IUsersService, UsersService>();
        }
    }
}
