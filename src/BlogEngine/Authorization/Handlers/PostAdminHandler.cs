using BlogEngine.Authorization.Requirements;
using BlogEngine.DTO.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogEngine.Authorization.Handlers
{
    public class PostAdminHandler : AuthorizationHandler<PostOperationRequirement, BlogPost>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PostOperationRequirement requirement, BlogPost post)
        {
            if (context.User.IsAdmin())
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
