using BlogEngine.DTO;

namespace System.Security.Claims
{
    /// <summary>
    /// Extensions for <see cref="ClaimsPrincipal"/>
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAuthenticated(this ClaimsPrincipal user)
        {
            return user?.Identity?.IsAuthenticated == true;
        }

        public static int GetId(this ClaimsPrincipal user)
        {
            var idAsString = user?.FindFirst(BlogConstants.Claims.UserId)?.Value;
            int.TryParse(idAsString, out var id);

            return id;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user?.HasClaim(BlogConstants.Claims.Admin, BlogConstants.Claims.Admin) == true;
        }

        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user?.FindFirst(BlogConstants.Claims.Username)?.Value;
        }

        public static string GetDetailsStamp(this ClaimsPrincipal user)
        {
            return user?.FindFirst(BlogConstants.Claims.DetailsStamp)?.Value;
        }
    }
}
