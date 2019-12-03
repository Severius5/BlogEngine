using BlogEngine.Core.Providers;
using BlogEngine.Core.Results;
using BlogEngine.DTO;
using BlogEngine.Storage.Repositories;
using System;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services.Internals
{
    internal class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<SignInResult> SignIn(string email, string password)
        {
            var user = await _userRepository.GetUser(email);
            if (user == null)
                return new SignInResult(ErrorCodes.InvalidCredentials);

            if (user.IsBlocked)
                return new SignInResult(ErrorCodes.UserBlocked);

            if (!PasswordHasher.VerifyHashedPassword(user.Password, password))
                return new SignInResult(ErrorCodes.InvalidCredentials);

            var principal = ClaimsProvider.GenerateClaimsPrincipal(user, "login");

            return new SignInResult(principal);
        }
    }
}
