using Folio3.Sbp.Data.AuditLogging;

namespace Folio3.Sbp.Api.Provider
{
    public class AuditMetaData : IAuditMetaData
    {
        public string UserEmail { get; } = "obaid@live.com";
        public string UserName { get; } = "Obaid";
    }
}