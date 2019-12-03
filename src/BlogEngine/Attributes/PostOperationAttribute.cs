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
    public class PostOperationAttribute : TypeFilterAttribute
    {
        public PostOperationAttribute(PostOperation postOperation)
            : base(typeof(PostOperationAttributeImpl))
        {
            Arguments = new object[] { postOperation };
        }

        private class PostOperationAttributeImpl : Attribute, IAsyncAuthorizationFilter
        {
            private readonly IAuthorizationService _authorizationService;
            private readonly IPostsService _postsService;
            private readonly PostOperation _postOperation;

            public PostOperationAttributeImpl(IAuthorizationService authorizationService, IPostsService postsService, PostOperation postOperation)
            {
                _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
                _postsService = postsService ?? throw new ArgumentNullException(nameof(postsService));
                _postOperation = postOperation;
            }

            public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
            {
                var blogPost = await _postsService.GetPost(context.GetBlogPostIdFromRoute());
                if (blogPost == null)
                {
                    context.Result = new NotFoundResult();
                    return;
                }

                var authorizeResult = await _authorizationService.AuthorizeAsync(
                    context.HttpContext.User,
                    blogPost,
                    new PostOperationRequirement(_postOperation));

                if (!authorizeResult.Succeeded)
                {
                    if (_postOperation == PostOperation.View)
                        context.Result = new NotFoundResult();
                    else
                        context.Result = new ForbidResult();
                }
            }
        }
    }
}
