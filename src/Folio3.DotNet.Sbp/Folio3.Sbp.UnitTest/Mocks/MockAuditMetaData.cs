using Folio3.Sbp.Data.AuditLogging;

namespace Folio3.Sbp.UnitTest.Mocks
{
	public class MockAuditMetaData : IAuditMetaData
	{
		public string UserEmail => "unittestuser@folio3.com";

		public string UserName => "Unit Test User";
	}
}
