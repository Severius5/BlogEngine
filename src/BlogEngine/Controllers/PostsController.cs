using BlogEngine.Attributes;
using BlogEngine.Authorization;
using BlogEngine.Core.Services;
using BlogEngine.DTO.Models;
using BlogEngine.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;

namespace BlogEngine.Controllers
{
    [Authorize]
    [Route("management/posts")]
    public class PostsController : BlogBaseController
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService ?? throw new ArgumentNullException(nameof(postsService));
        }

        [HttpGet]
        public async Task<IActionResult> Posts([FromQuery] PostsFilter filter)
        {
            filter = filter ?? new PostsFilter();
            filter.AuthorId = filter.Own ? User.GetId() : null as int?;
            filter.Removed = User.IsAdmin() ? filter.Removed : false;

            var result = await _postsService.GetPosts(filter);
            var postsVM = result.Elements?.Select(x => (PostViewModel)x)?.ToList() ?? Enumerable.Empty<PostViewModel>();
            var pagedPostsVM = new StaticPagedList<PostViewModel>(postsVM, filter.Page, filter.PageSize, result.TotalElements);

            return View(new PostsListViewModel
            {
                Filter = filter,
                Posts = pagedPostsVM
            });
        }

        [HttpGet("create")]
        public IActionResult NewPost()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> NewPost([FromForm] CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _postsService.CreatePost(model.Title, model.Content, model.Description, model.Publish, User.GetId());
            if (result.IsError)
            {
                ModelState.AddModelError(string.Empty, result.ErrorCode);
                return View(model);
            }

            return RedirectToPost(result.PostId, result.PostSlug);
        }

        [PostOperation(PostOperation.Edit)]
        [HttpGet("edit/{postId:int}")]
        public async Task<IActionResult> EditPost([FromRoute] int postId)
        {
            var post = await _postsService.GetPost(postId);
            if (post == null)
                return NotFound();

            return View(new EditPostViewModel
            {
                Content = post.Content,
                Description = post.Description,
                Title = post.Title
            });
        }

        [PostOperation(PostOperation.Edit)]
        [HttpPost("edit/{postId:int}")]
        public async Task<IActionResult> EditPost([FromRoute] int postId, [FromForm] EditPostViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _postsService.EditPost(postId, model.Title, model.Content, model.Description);
            if (result.IsError)
            {
                ModelState.AddModelError(string.Empty, result.ErrorCode);
                return View(model);
            }

            return RedirectToPost(result.PostId, result.PostSlug);
        }

        [PostOperation(PostOperation.Remove)]
        [HttpPost("remove/{postId:int}")]
        public async Task<IActionResult> RemovePost([FromRoute] int postId, [FromQuery] string returnUrl)
        {
            var result = await _postsService.RemovePost(postId);
            //if (result.IsError)
            //{
            //    ModelState.AddModelError(string.Empty, result.ErrorCode);
            //    return PartialView();
            //}

            return RedirectToLocal(returnUrl);
        }

        [PostOperation(PostOperation.Delete)]
        [HttpPost("delete/{postId:int}")]
        public async Task<IActionResult> DeletePost([FromRoute] int postId, [FromQuery] string returnUrl)
        {
            var result = await _postsService.DeletePost(postId);
            //if (result.IsError)
            //{
            //    ModelState.AddModelError(string.Empty, result.ErrorCode);
            //    return PartialView();
            //}

            return RedirectToLocal(returnUrl);
        }

        [PostOperation(PostOperation.Restore)]
        [HttpPost("restore/{postId:int}")]
        public async Task<IActionResult> RestorePost([FromRoute] int postId, [FromQuery] string returnUrl)
        {
            var result = await _postsService.RestorePost(postId);
            //if (result.IsError)
            //{
            //    ModelState.AddModelError(string.Empty, result.ErrorCode);
            //    return PartialView();
            //}

            return RedirectToLocal(returnUrl);
        }

        [PostOperation(PostOperation.Publish)]
        [HttpPost("publish/{postId:int}")]
        public async Task<IActionResult> PublishPost([FromRoute] int postId, [FromQuery] string returnUrl)
        {
            var result = await _postsService.PublishPost(postId);
            //if (result.IsError)
            //{
            //    ModelState.AddModelError(string.Empty, result.ErrorCode);
            //    return PartialView();
            //}

            return RedirectToLocal(returnUrl);
        }

        [PostOperation(PostOperation.Unpublish)]
        [HttpPost("unpublish/{postId:int}")]
        public async Task<IActionResult> UnpublishPost([FromRoute] int postId, [FromQuery] string returnUrl)
        {
            var result = await _postsService.UnpublishPost(postId);
            //if (result.IsError)
            //{
            //    ModelState.AddModelError(string.Empty, result.ErrorCode);
            //    return PartialView();
            //}

            return RedirectToLocal(returnUrl);
        }
    }
}
