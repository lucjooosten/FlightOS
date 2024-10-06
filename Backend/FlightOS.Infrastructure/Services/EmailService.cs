using FlightOS.Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace FlightOS.Infrastructure.Services
{
    /// <summary>
    /// Service for sending emails.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="recipientEmail"></param>
        /// <param name="subject"></param>
        /// <param name="messageBody"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string recipientEmail, string subject, string messageBody)
        {
            // Load SMTP settings from the configuration
            var smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(smtpSettings!.SenderName, smtpSettings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(recipientEmail));
            message.Subject = subject;

            // Ensure we're setting the HTML body here
            var bodyBuilder = new BodyBuilder { HtmlBody = messageBody };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, smtpSettings.UseSsl);
            await client.AuthenticateAsync(smtpSettings.Username, smtpSettings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        /// <summary>
        /// Represents the SMTP settings for sending emails.
        /// </summary>
        public sealed class SmtpSettings
        {
            public string Server { get; set; } = string.Empty;
            public int Port { get; set; }
            public string SenderName { get; set; } = string.Empty;
            public string SenderEmail { get; set; } = string.Empty;
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public bool UseSsl { get; set; }
        }
    }
}
