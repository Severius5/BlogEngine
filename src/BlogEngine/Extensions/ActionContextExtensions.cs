using System;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// Extensions for <see cref="ActionContext"/>
    /// </summary>
    public static class ActionContextExtensions
    {
        public static int GetBlogPostIdFromRoute(this ActionContext context)
        {
            var stringId = context.RouteData.Values["postId"] as string;
            if (int.TryParse(stringId, out var postId))
                return postId;
            else
                throw new InvalidOperationException("Route data does not contain postId");
        }

        public static int GetBlogUserIdFromRoute(this ActionContext context)
        {
            var stringId = context.RouteData.Values["userId"] as string;
            if (int.TryParse(stringId, out var userId))
                return userId;
            else
                throw new InvalidOperationException("Route data does not contain userId");
        }
    }
}
