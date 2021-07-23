using Folio3.Sbp.UnitTest.Helpers;
using Microsoft.EntityFrameworkCore;
using System;

namespace Folio3.Sbp.UnitTest.Mocks
{
    public class MockContext<TContext> : IDisposable where TContext : DbContext
    {
        private SqliteMemoryDbManager<TContext> SqliteMemoryDbManager { get; }

        public TContext Context { get; }

        public MockContext(Func<DbContextOptions<TContext>, TContext> creator)
        {
            SqliteMemoryDbManager = new SqliteMemoryDbManager<TContext>();
            Context = creator(SqliteMemoryDbManager.Options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Dispose();
            SqliteMemoryDbManager.Dispose();
        }
    }
}
