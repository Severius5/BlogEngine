using BlogEngine.Authorization.Requirements;
using BlogEngine.DTO.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogEngine.Authorization.Handlers
{
    public class UserAdminHandler : AuthorizationHandler<UserOperationRequirement, BlogUser>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserOperationRequirement requirement, BlogUser user)
        {
            if (context.User.IsAdmin())
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
