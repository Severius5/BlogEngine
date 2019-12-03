using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Controllers
{
    public abstract class BlogBaseController : Controller
    {
        public IActionResult RedirectToLocal(string url)
        {
            if (Url.IsLocalUrl(url))
                return Redirect(url);
            else
                return RedirectToHome();
        }

        public IActionResult RedirectToHome()
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult RedirectToPost(int postId, string slug)
        {
            return RedirectToAction(nameof(HomeController.Post), "Home", new { postId, slug });
        }

        public IActionResult RedirectToAuthor(int userId, string slug)
        {
            return RedirectToAction(nameof(HomeController.Author), "Home", new { userId, slug });
        }
    }
}
