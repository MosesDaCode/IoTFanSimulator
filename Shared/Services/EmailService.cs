using Azure;
using Azure.Communication.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class EmailService
    {
        private readonly EmailClient _emailClient;

        public EmailService()
        {
            _emailClient = new EmailClient(AppConfig.EmailConnectionString);
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailContent = new EmailContent(subject)
            {
                PlainText = body
            };

            var emailRecipients = new EmailRecipients(new List<EmailAddress>
            {
                new EmailAddress(toEmail)
            });

            var emailMessage = new EmailMessage("DoNotReply@58163f00-b623-4580-9a2b-bded8c5d66c4.azurecomm.net", emailRecipients, emailContent);

            var response = await _emailClient.SendAsync(WaitUntil.Completed, emailMessage);
        }
    }
}
