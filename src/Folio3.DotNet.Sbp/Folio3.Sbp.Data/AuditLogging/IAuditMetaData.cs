namespace Folio3.DotNet.Sbp.Data.AuditLogging
{
    public interface IAuditMetaData
    {
        string UserEmail { get; }
        string UserName { get; }
    }
}
