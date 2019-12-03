using BlogEngine.Attributes;
using BlogEngine.Authorization;
using BlogEngine.Core.Services;
using BlogEngine.DTO.Models;
using BlogEngine.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace BlogEngine.Controllers
{
    [Authorize]
    [Route("management/authors")]
    public class UsersController : BlogBaseController
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [HttpGet]
        public async Task<IActionResult> Authors([FromQuery] UsersFilter filter)
        {
            filter = filter ?? new UsersFilter();

            var result = await _usersService.GetUsers(filter);
            var usersVM = result.Elements?.Select(x => (UserViewModel)x)?.ToList() ?? Enumerable.Empty<UserViewModel>();
            var pagedUsersVM = new StaticPagedList<UserViewModel>(usersVM, filter.Page, filter.PageSize, result.TotalElements);

            return View(new UsersListViewModel
            {
                Filter = filter,
                Users = pagedUsersVM
            });
        }

        [HttpGet("edit/{userId:int}")]
        [UserOperation(UserOperation.Edit)]
        public async Task<IActionResult> EditUser([FromRoute] int userId)
        {
            var user = await _usersService.GetUser(userId);
            if (user == null)
                return NotFound();

            return View(new EditUserViewModel
            {
                Bio = user.Bio,
                Username = user.Username
            });
        }

        [UserOperation(UserOperation.Edit)]
        [HttpPost("edit/{userId:int}")]
        public async Task<IActionResult> EditUser([FromRoute] int userId, [FromForm] EditUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _usersService.EditUser(userId, model.Username, model.Bio);
            if (result.IsError)
            {
                ModelState.AddModelError(string.Empty, result.ErrorCode);
                return View(model);
            }

            return RedirectToAuthor(result.UserId, result.UserSlug);
        }

        [UserOperation(UserOperation.Block)]
        [HttpPost("block/{userId:int}")]
        public async Task<IActionResult> BlockUser([FromRoute] int userId, [FromQuery] string returnUrl)
        {
            var result = await _usersService.BlockUser(userId);

            return RedirectToLocal(returnUrl);
        }

        [UserOperation(UserOperation.Unblock)]
        [HttpPost("unblock/{userId:int}")]
        public async Task<IActionResult> UnblockUser([FromRoute] int userId, [FromQuery] string returnUrl)
        {
            var result = await _usersService.UnblockUser(userId);

            return RedirectToLocal(returnUrl);
        }

        [UserOperation(UserOperation.ChangeAdminStatus)]
        [HttpPost("edit/{userId:int}/status/admin")]
        public async Task<IActionResult> ChangeAdminStatus([FromRoute] int userId, [FromForm] bool admin, [FromQuery] string returnUrl)
        {
            var result = await _usersService.ChangeUserAdminStatus(userId, admin);

            return RedirectToLocal(returnUrl);
        }
    }
}
