using BlogEngine.Notifications.Models;
using BlogEngine.Notifications.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace BlogEngine.Notifications.Senders
{
    internal class EmailSender : INotificationSender
    {
        private readonly EmailOptions _options;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IOptionsSnapshot<EmailOptions> options, ILogger<EmailSender> logger)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Send(Notification notification)
        {
            if (!_options.Enabled)
            {
                _logger.LogDebug("Email sender is disabled and wont send notification");
                return true;
            }

            var message = new MimeMessage
            {
                Subject = notification.Subject,
                Body = new TextPart(_options.TextFormat)
                {
                    Text = notification.Body
                }
            };

            message.From.Add(new MailboxAddress(_options.SenderName, _options.SenderAddress));
            foreach (var recipient in notification.Recipients)
                message.Bcc.Add(new MailboxAddress(recipient));

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_options.Host, _options.Port, _options.UseSsl);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_options.Username, _options.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while sending email: " + ex.Message);
                return false;
            }
        }
    }
}
