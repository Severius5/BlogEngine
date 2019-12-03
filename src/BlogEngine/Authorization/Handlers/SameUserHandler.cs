using BlogEngine.Authorization.Requirements;
using BlogEngine.DTO.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogEngine.Authorization.Handlers
{
    public class SameUserHandler : AuthorizationHandler<UserOperationRequirement, BlogUser>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserOperationRequirement requirement, BlogUser user)
        {
            if(!IsSupportedOperation(requirement.Operation))
                return Task.CompletedTask;

            if (context.User.GetId() == user.Id)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

        private bool IsSupportedOperation(UserOperation operation)
        {
            return operation == UserOperation.Edit;
        }
    }
}
