using BlogEngine.Authorization;
using BlogEngine.Authorization.Requirements;
using BlogEngine.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace BlogEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class UserOperationAttribute : TypeFilterAttribute
    {
        public UserOperationAttribute(UserOperation userOperation)
            : base(typeof(UserOperationAttributeImpl))
        {
            Arguments = new object[] { userOperation };
        }

        private class UserOperationAttributeImpl : Attribute, IAsyncAuthorizationFilter
        {
            private readonly IAuthorizationService _authorizationService;
            private readonly IUsersService _usersService;
            private readonly UserOperation _userOperation;

            public UserOperationAttributeImpl(IAuthorizationService authorizationService, IUsersService usersService, UserOperation userOperation)
            {
                _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
                _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
                _userOperation = userOperation;
            }

            public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
            {
                var blogUser = await _usersService.GetUser(context.GetBlogUserIdFromRoute());
                if (blogUser == null)
                {
                    context.Result = new NotFoundResult();
                    return;
                }

                var authorizeResult = await _authorizationService.AuthorizeAsync(
                    context.HttpContext.User,
                    blogUser,
                    new UserOperationRequirement(_userOperation));

                if (!authorizeResult.Succeeded)
                    context.Result = new ForbidResult();
            }
        }
    }
}
