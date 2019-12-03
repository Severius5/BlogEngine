using BlogEngine.Authorization;
using BlogEngine.Authorization.Handlers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogAuthorization(this IServiceCollection services)
        {
            return services
                .AddSingleton<IAuthorizationHandler, PostAdminHandler>()
                .AddSingleton<IAuthorizationHandler, PostAuthorHandler>()
                .AddSingleton<IAuthorizationHandler, ViewPostHandler>()
                .AddSingleton<IAuthorizationHandler, SameUserHandler>()
                .AddSingleton<IAuthorizationHandler, UserAdminHandler>();
        }

        public static IServiceCollection AddBlogAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
                {
                    opt.Cookie.HttpOnly = true;
                    opt.Cookie.SameSite = SameSiteMode.Strict;
                    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    opt.Cookie.IsEssential = true;
                    opt.ExpireTimeSpan = TimeSpan.FromDays(14);
                    opt.EventsType = typeof(CustomCookieAuthEvents);
                });

            return services
                .AddScoped<CustomCookieAuthEvents>();
        }
    }
}
