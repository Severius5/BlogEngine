using BlogEngine.Authorization.Requirements;
using BlogEngine.DTO.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogEngine.Authorization.Handlers
{
    public class PostAuthorHandler : AuthorizationHandler<PostOperationRequirement, BlogPost>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PostOperationRequirement requirement, BlogPost post)
        {
            if (!IsSupportedOperation(requirement.Operation))
                return Task.CompletedTask;

            if (context.User.GetId() == post.Author.Id)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

        private bool IsSupportedOperation(PostOperation operation)
        {
            return operation == PostOperation.Remove
                || operation == PostOperation.Edit
                || operation == PostOperation.Publish
                || operation == PostOperation.Unpublish;
        }
    }
}
