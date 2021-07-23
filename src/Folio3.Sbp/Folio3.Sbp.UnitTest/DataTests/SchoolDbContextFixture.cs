using Folio3.Sbp.Data.AuditLogging;
using Folio3.Sbp.Data.School;
using Folio3.Sbp.UnitTest.Helpers;
using Folio3.Sbp.UnitTest.Mocks;
using Microsoft.EntityFrameworkCore;
using System;

namespace Folio3.Sbp.UnitTest.DataTests
{
	public class SchoolDbContextFixture : IDisposable
    {
        public SchoolDbContext SchoolDbContext { get; }
        public AuditLogDbContext AuditLogDbContext { get; }

        private readonly SqliteMemoryDbManager<SchoolDbContext> SqliteMemoryDbManager;

        public SchoolDbContextFixture()
        {
            var builder = new DbContextOptionsBuilder<AuditLogDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryAuditLog");

            AuditLogDbContext = new AuditLogDbContext(builder.Options);
            AuditLogDbContext.Database.EnsureCreated();

            SqliteMemoryDbManager = new SqliteMemoryDbManager<SchoolDbContext>();

            SchoolDbContext = new SchoolDbContext(
                options: SqliteMemoryDbManager.Options,
                auditLogDbContext: AuditLogDbContext,
                logger: new MockLogger<SchoolDbContext>(),
                auditMetaData: new MockAuditMetaData());

            SchoolDbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            AuditLogDbContext.Dispose();
            SchoolDbContext.Dispose();
            SqliteMemoryDbManager.Dispose();
        }
    }
}
