using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Folio3.Sbp.Common.Email
{
	/// <summary>
	/// Extended IEmailSender.
	/// This also allows to send email to multiple recipients using To, Cc and Bcc collection
	/// </summary>
	public interface IExtendedEmailSender : IEmailSender
	{
		Task<bool> SendEmailAsync(MailMessage message);
	}
}
