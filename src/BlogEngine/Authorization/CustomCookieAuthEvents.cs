using BlogEngine.Core.Providers;
using BlogEngine.Core.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogEngine.Authorization
{
    /// <inheritdoc/>
    public class CustomCookieAuthEvents : CookieAuthenticationEvents
    {
        private readonly IUsersService _usersService;

        public CustomCookieAuthEvents(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        /// <inheritdoc/>
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var user = await _usersService.GetUser(context.Principal.GetId());
            if (user?.IsBlocked != false)
            {
                context.RejectPrincipal();

                return;
            }

            if (!string.Equals(user.DetailsStamp, context.Principal.GetDetailsStamp()))
            {
                var newPrincipal = ClaimsProvider.GenerateClaimsPrincipal(user, context.Principal.Identity.AuthenticationType);
                context.ReplacePrincipal(newPrincipal);
                context.ShouldRenew = true;

                return;
            }

            await base.ValidatePrincipal(context);
        }
    }
}
