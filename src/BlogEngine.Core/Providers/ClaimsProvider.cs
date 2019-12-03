using BlogEngine.DTO;
using BlogEngine.DTO.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace BlogEngine.Core.Providers
{
    public static class ClaimsProvider
    {
        public static ClaimsPrincipal GenerateClaimsPrincipal(BlogUser user, string authType)
        {
            var claims = GetUserClaims(user);

            return new ClaimsPrincipal(new ClaimsIdentity(claims, authType));
        }

        private static List<Claim> GetUserClaims(BlogUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(BlogConstants.Claims.UserId, user.Id.ToString()),
                new Claim(BlogConstants.Claims.Username, user.Username),
                new Claim(BlogConstants.Claims.DetailsStamp, user.DetailsStamp)
            };

            if (user.IsAdmin)
                claims.Add(new Claim(BlogConstants.Claims.Admin, BlogConstants.Claims.Admin));

            return claims;
        }
    }
}
