using System.Threading.Tasks;

namespace BlogEngine.Notifications.Services
{
    internal interface IViewRenderService
    {
        Task<string> RenderToString(string viewName);
        Task<string> RenderToString<TModel>(string viewName, TModel viewModel);
    }
}
