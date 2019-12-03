using BlogEngine.Notifications.ViewModels;

namespace BlogEngine.Notifications.Models.Templates
{
    public class EmailTemplate
    {
        private const string ViewTemplate = "Views/Emails/{0}.cshtml";

        public EmailTemplate(string[] recipients, string subject, string viewName, BaseViewModel viewModel = null)
        {
            Recipients = recipients;
            Subject = subject;
            ViewName = string.Format(ViewTemplate, viewName);
            ViewModel = viewModel;
        }

        public string ViewName { get; }
        public BaseViewModel ViewModel { get; }
        public string Subject { get; }
        public string[] Recipients { get; }
    }
}
