using BlogEngine.Core.Results;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services
{
    public interface IAuthService
    {
        Task<SignInResult> SignIn(string email, string password);
    }
}
