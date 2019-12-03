using BlogEngine.Authorization.Requirements;
using BlogEngine.DTO.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogEngine.Authorization.Handlers
{
    public class ViewPostHandler : AuthorizationHandler<PostOperationRequirement, BlogPost>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PostOperationRequirement requirement, BlogPost post)
        {
            if (requirement.Operation != PostOperation.View)
                return Task.CompletedTask;

            switch (post.Status)
            {
                case PostStatus.Published:
                    context.Succeed(requirement);
                    break;

                case PostStatus.Draft:
                    if (context.User.IsAuthenticated())
                        context.Succeed(requirement);
                    break;

                case PostStatus.Removed:
                    if (context.User.IsAdmin())
                        context.Succeed(requirement);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
