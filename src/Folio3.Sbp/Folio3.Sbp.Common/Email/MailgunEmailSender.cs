using Folio3.Sbp.Common.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Folio3.Sbp.Common.Email
{
	/// <summary>
	/// Mailgun IExtendedEmailSender implementation
	/// </summary>
	public class MailgunEmailSender : IExtendedEmailSender
	{
		private readonly IHttpClientFactory ClientFactory;
		private readonly MailgunSettings MailgunSettings;

		public MailgunEmailSender(IHttpClientFactory clientFactory, IOptions<MailgunSettings> mailgunSettings)
		{
			ClientFactory = clientFactory;
			MailgunSettings = mailgunSettings.Value;
		}

		public async Task<bool> SendEmailAsync(MailMessage message)
		{
			var request = new HttpRequestMessage(HttpMethod.Post, $"{MailgunSettings.BaseUrl}/{MailgunSettings.Domain}/messages");

			request.Headers.Authorization = new AuthenticationHeaderValue("Basic",
				Convert.ToBase64String(Encoding.UTF8.GetBytes($"api:{MailgunSettings.ApiKey}")));

			var form = new Dictionary<string, string>
			{
				["from"] = MailgunSettings.User,
				["to"] = message.To.ToString(),
				["subject"] = message.Subject,
				["html"] = message.Body
			};

			if (message.CC.Any())
				form["cc"] = message.CC.ToString();

			if (message.Bcc.Any())
				form["bcc"] = message.Bcc.ToString();

			request.Content = new FormUrlEncodedContent(form);

			var response = await ClientFactory.CreateClient().SendAsync(request);

			return response.IsSuccessStatusCode;
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
