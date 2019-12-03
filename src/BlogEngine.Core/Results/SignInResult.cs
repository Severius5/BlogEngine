using System.Security.Claims;

namespace BlogEngine.Core.Results
{
    public class SignInResult : ErrorResult
    {
        public ClaimsPrincipal Principal { get; }

        public SignInResult(string errorCode)
        {
            ErrorCode = errorCode;
        }

        public SignInResult(ClaimsPrincipal principal)
        {
            Principal = principal;
        }
    }
}
