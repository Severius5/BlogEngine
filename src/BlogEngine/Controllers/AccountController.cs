using BlogEngine.Core.Services;
using BlogEngine.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogEngine.Controllers
{
    [Route("account")]
    [AutoValidateAntiforgeryToken]
    public class AccountController : BlogBaseController
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpGet("login")]
        public IActionResult Login([FromQuery] string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model, [FromQuery] string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.SignIn(model.Email, model.Password);
            if (result.IsError)
            {
                ModelState.AddModelError(string.Empty, result.ErrorCode);
                return View(model);
            }

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal);
            HttpContext.User = result.Principal;

            return RedirectToLocal(returnUrl);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToHome();
        }
    }
}
