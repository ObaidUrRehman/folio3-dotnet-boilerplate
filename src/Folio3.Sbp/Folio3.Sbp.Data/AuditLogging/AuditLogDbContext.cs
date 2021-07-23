using Folio3.Sbp.Data.AuditLogging.Entities;
using Microsoft.EntityFrameworkCore;

namespace Folio3.Sbp.Data.AuditLogging
{
    public class AuditLogDbContext : DbContext
    {
        public AuditLogDbContext(DbContextOptions<AuditLogDbContext> options)
            : base(options)
        {
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<AuditLogChange> AuditLogChanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuditLog>().HasIndex(log => new {log.UserEmail}).IsUnique(false);
            modelBuilder.Entity<AuditLog>().HasIndex(log => new {log.TableName}).IsUnique(false);
            modelBuilder.Entity<AuditLog>().HasIndex(log => new {log.RecordId}).IsUnique(false);
            modelBuilder.Entity<AuditLog>().HasIndex(log => new {log.TimeStamp}).IsUnique(false);
        }
    }
}