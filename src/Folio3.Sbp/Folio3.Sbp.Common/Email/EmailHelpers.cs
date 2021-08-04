using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Folio3.Sbp.Common.Email
{
	public static class EmailHelpers
	{
		/// <summary>
		/// Convert a System.Net.Mail MailAddress to a string
		/// </summary>
		public static string Convert(MailAddress ma) => ma.Address;

		public static List<string> Convert(IEnumerable<MailAddress> list) => list.Select(Convert).ToList();
	}
}
