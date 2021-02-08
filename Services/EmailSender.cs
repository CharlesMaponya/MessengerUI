using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerUI.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendMail(string email, string subject, string body)
        {
            return Execute("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", subject, body, email);
        }

        private Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("daemongriffons@gmail.com", "Messenger UI"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message,
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
