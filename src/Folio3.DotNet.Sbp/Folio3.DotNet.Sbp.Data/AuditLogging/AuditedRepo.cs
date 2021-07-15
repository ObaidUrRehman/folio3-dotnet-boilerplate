using System.Threading;
using System.Threading.Tasks;
using Folio3.DotNet.Sbp.Data.School.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Folio3.DotNet.Sbp.Data.AuditLogging
{
    public class AuditedDbContext: IdentityDbContext<User>
    {
        private AuditLogDbContext AuditLogDbContext { get; }
        private AuditLogger AuditLogger { get; }
        private IAuditMetaData AuditMetaData { get; }

        protected ILogger Logger { get; }

        public AuditedDbContext(DbContextOptions options, AuditLogDbContext auditLogDbContext, ILogger logger,
            AuditLogger auditLogger, IAuditMetaData auditMetaData) : base(options)
        {
            Logger = logger;
            AuditLogDbContext = auditLogDbContext;
            AuditLogger = auditLogger;
            AuditMetaData = auditMetaData;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await AuditLogger.SaveAndAuditAsync(AuditLogDbContext, AuditMetaData);
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
