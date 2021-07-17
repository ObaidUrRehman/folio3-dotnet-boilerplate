using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Folio3.Sbp.Data.AuditLogging.Entities;
using Folio3.Sbp.Data.AuditLogging.Extensions;
using Folio3.Sbp.Data.School.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace Folio3.Sbp.Data.AuditLogging
{
    public class AuditedDbContext: IdentityDbContext<User>
    {
        private AuditLogDbContext AuditLogDbContext { get; }
        private IAuditMetaData AuditMetaData { get; }

        protected ILogger Logger { get; }

        public AuditedDbContext(DbContextOptions options, AuditLogDbContext auditLogDbContext, 
            ILogger logger,IAuditMetaData auditMetaData) : base(options)
        {
            Logger = logger;
            AuditLogDbContext = auditLogDbContext;
            AuditMetaData = auditMetaData;
        }

        public override int SaveChanges()
        {
            return SaveChanges(true);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new Exception("Use SaveChangesAsync() instead of SaveChanges()");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await SaveChangesAsync(true, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            // pull initial set of tracked changes and generate Audit Logs
            var dbEntries = ChangeTracker.Entries()
                .Where(dbEntry => dbEntry.State != EntityState.Unchanged)
                .ToList();

            IList<AuditLog> auditLogs = GenerateLogs(dbEntries);

            // commit the changes (without the Audit Logs)
            int result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            if (!auditLogs.Any())
                return result;

            // RJE kludge: use the post save to match up records and set RecordId for added records
            var auditLogsPost = GenerateLogs(dbEntries);

            try
            {
                if (auditLogs.Count == auditLogsPost.Count)
                {
                    for (int i = 0; i < auditLogs.Count; i++)
                        if (auditLogs[i].Id == 0 && auditLogs[i].EntityState == EntityState.Added)
                            auditLogs[i].RecordId = auditLogsPost[i].RecordId;
                }

                // now commit the Audit Logs
                await AuditLogDbContext.AuditLogs.AddRangeAsync(auditLogs, cancellationToken);
                await AuditLogDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                // Exception in writing log must not affect normal flow.
                Logger.LogError(e, "Error Adding AuditLog");
            }

            return result;

            IList<AuditLog> GenerateLogs(IList<EntityEntry> dbEntries)
                => GenerateAuditLogs(dbEntries)
                    .Select(auditLog =>
                    {
                        auditLog.UserEmail = AuditMetaData?.UserEmail;
                        auditLog.UserName = AuditMetaData?.UserName;
                        return auditLog;
                    })
                    .ToList();
        }

        /// <summary>
        /// Generate a detailed Audit Logs of changes to tracked entities
        /// </summary>
        public IEnumerable<AuditLog> GenerateAuditLogs(IEnumerable<EntityEntry> dbEntries)
        {
            try
            {
                return dbEntries.Select(GenerateAuditLog).ToList();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error Generating Audit Logs.");
                return new List<AuditLog>();
            }
        }

        private AuditLog GenerateAuditLog(EntityEntry dbEntry)
        {
            object orig = GetOriginalEntity(dbEntry);
            object curr = GetCurrentEntity(dbEntry);

            return new AuditLog
            {
                EntityState = dbEntry.State,
                TableName = dbEntry.GetTableName(),
                RecordId = dbEntry.GetPrimaryKeyValue(),
                TimeStamp = DateTime.UtcNow,
                AuditLogChanges = dbEntry
                    .CurrentValues
                    .Properties
                    .Where(prop => !prop.IsIndexerProperty())
                    .Select(prop => new AuditLogChange
                    {
                        Property = prop.Name,
                        Original = GetPropertyValue(orig, prop.Name),
                        Current = GetPropertyValue(curr, prop.Name),
                    })
                    .Where(change => change.Original != change.Current)
                    .ToList(),
            };
        }

        private static object GetOriginalEntity(EntityEntry dbEntry)
        {
            switch (dbEntry.State)
            {
                case EntityState.Modified:
                case EntityState.Deleted:
                    return dbEntry.OriginalValues.ToObject();

                default:
                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Added:
                    return null;
            };
        }

        private static object GetCurrentEntity(EntityEntry dbEntry)
        {
            switch (dbEntry.State)
            {
                case EntityState.Modified:
                case EntityState.Added:
                    return dbEntry.CurrentValues.ToObject();

                default:
                case EntityState.Deleted:
                case EntityState.Detached:
                case EntityState.Unchanged:
                    return null;
            };
        }

        private string GetPropertyValue(object entity, string name)
        {
            try
            {
                if (entity == null)
                    return "";

                object o = entity
                    .GetType()
                    .GetProperty(name)
                    .GetValue(entity, null);

                return o == null ? "" : o.ToString();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error resolving property {name} in entity [{entity}].");
                return "";
            }
        }
    }
}
