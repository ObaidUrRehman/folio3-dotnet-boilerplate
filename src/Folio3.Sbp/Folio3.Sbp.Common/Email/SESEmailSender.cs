using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Folio3.Sbp.Common.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Folio3.Sbp.Common.Email
{
    /// <summary>
    /// AWS Simple Email Service (SES) IEmailSender implementation
    /// </summary>
    public class SESEmailSender : IExtendedEmailSender
    {
        private readonly SimpleEmailServiceSettings SESSettings;

        public SESEmailSender(IOptions<SimpleEmailServiceSettings> simpleEmailServiceSettings)
        {
            SESSettings = simpleEmailServiceSettings.Value;
        }

        /// <summary>
		/// Convert a System.Net.Mail MailAddress to a string
		/// </summary>
		private static string Convert(MailAddress ma) => ma.Address;

        private static List<string> Convert(IEnumerable<MailAddress> list) => list.Select(Convert).ToList();

        public async Task<bool> SendEmailAsync(MailMessage message)
        {
            using var client = new AmazonSimpleEmailServiceClient(SESSettings.AccessKey, SESSettings.SecretKey, SESSettings.Region);

            var emailRequest = new SendEmailRequest
            {
                Source = message.From.ToString(),
                Destination = new Destination(Convert(message.To)),
                Message = new Message
                {
                    Subject = new Content(message.Subject),
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Charset = "UTF-8",
                            Data = message.Body
                        }
                    }
                },
            };

            if (message.Bcc.Any())
                emailRequest.Destination.BccAddresses = Convert(message.Bcc);

            if (message.CC.Any())
                emailRequest.Destination.CcAddresses = Convert(message.CC);

            var response = await client.SendEmailAsync(emailRequest);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
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
