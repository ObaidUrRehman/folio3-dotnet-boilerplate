using Folio3.DotNet.Sbp.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Folio3.DotNet.Sbp.Data.Base
{
	public class BaseDbContext : DbContext
	{
		public BaseDbContext(DbContextOptions options) : base(options)
		{
		}

        public override int SaveChanges()
        {
            return SaveChanges(acceptAllChangesOnSuccess: true);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return Task.Run(async () => await SaveChangesAsync(acceptAllChangesOnSuccess)).Result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            TrackableHelpers.PopulateTrackableFields(changes: ChangeTracker);

            int result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            return result;
        }
    }
}
