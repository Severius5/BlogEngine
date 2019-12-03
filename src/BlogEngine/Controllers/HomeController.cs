using BlogEngine.Attributes;
using BlogEngine.Authorization;
using BlogEngine.Core.Services;
using BlogEngine.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlogEngine.Controllers
{
    public class HomeController : BlogBaseController
    {
        private readonly IPostsService _postsService;

        public HomeController(IPostsService postsService)
        {
            _postsService = postsService ?? throw new ArgumentNullException(nameof(postsService));
        }

        [HttpGet]
        public IActionResult Index(int? page, string search)
        {
            return View(); // lista postów
        }

        [PostOperation(PostOperation.View)]
        [HttpGet("post/{postId:int}/{slug?}")]
        public async Task<IActionResult> Post(int postId, string slug)
        {
            var post = await _postsService.GetPost(postId);
            if (post == null)
                return NotFound(); //todo: dedykowany widok

            if (!string.Equals(slug, post.Slug))
                return RedirectToActionPermanent(nameof(Post), new { postId, slug = post.Slug });

            return View((PostViewModel)post);
        }

        [HttpGet("category/{category}")]
        public IActionResult Category(string category)
        {
            throw new NotImplementedException();
        }

        [HttpGet("tag/{tag}")]
        public IActionResult Tag(string tag)
        {
            throw new NotImplementedException();
        }

        [HttpGet("author/{userId:int}/{slug?}")]
        public IActionResult Author(int userId, string slug)
        {
            throw new NotImplementedException();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
