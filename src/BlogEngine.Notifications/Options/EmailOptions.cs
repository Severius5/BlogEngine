using MimeKit.Text;

namespace BlogEngine.Notifications.Options
{
    internal class EmailOptions
    {
        public bool Enabled { get; set; }

        public string SenderName { get; set; }

        public string SenderAddress { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool UseSsl { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public TextFormat TextFormat { get; set; } = TextFormat.Html;
    }
}
