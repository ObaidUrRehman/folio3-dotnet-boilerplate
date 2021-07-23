namespace Folio3.Sbp.Data.AuditLogging
{
    public interface IAuditMetaData
    {
        string UserEmail { get; }
        string UserName { get; }
    }
}