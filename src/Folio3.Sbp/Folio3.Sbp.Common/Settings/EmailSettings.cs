namespace Folio3.Sbp.Common.Settings
{
	public class MailgunSettings
	{
		public string BaseUrl { get; set; }
		public string Domain { get; set; }
		public string ApiKey { get; set; }
		public string User { get; set; }
	}

	public class SimpleEmailServiceSettings
	{
		public string Region { get; set; }
		public string AccessKey { get; set; }
		public string SecretKey { get; set; }
	}
}
