using Folio3.Sbp.Common.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Folio3.Sbp.Common.Email
{
    /// <summary>
    /// SendGrid IEmailSender implementation
    /// </summary>
    public class SendGridEmailSender : IExtendedEmailSender
    {
        private readonly SendGridSettings SendGridSettings;

        public SendGridEmailSender(IOptions<SendGridSettings> sendGridSettings)
        {
            SendGridSettings = sendGridSettings.Value;
        }

        /// <summary>
        /// Convert a System.Net.Mail MailAddress to a SendGrid EmailAddress
        /// </summary>
        private static EmailAddress Convert(MailAddress ma) => new EmailAddress(ma.Address, ma.DisplayName);

        private static List<EmailAddress> Convert(IEnumerable<MailAddress> list) => list.Select(Convert).ToList();

        /// <summary>
        /// Send an email!
        /// </summary>
        public async Task<bool> SendEmailAsync(MailMessage message)
        {
            var client = new SendGridClient(SendGridSettings.ApiKey);

            var sgm = new SendGridMessage
            {
                From = Convert(message.From),
                Subject = message.Subject,
            };

            if (message.To.Any())
                sgm.AddTos(Convert(message.To));

            if (message.Bcc.Any())
                sgm.AddBccs(Convert(message.Bcc));

            sgm.AddContent(MimeType.Html, message.Body);

            var response = await client.SendEmailAsync(sgm);

            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new MailMessage()
            {
                Subject = subject,
                Body = message
            };

            mail.To.Add(new MailAddress(email));

            await SendEmailAsync(mail);
        }
    }
}
