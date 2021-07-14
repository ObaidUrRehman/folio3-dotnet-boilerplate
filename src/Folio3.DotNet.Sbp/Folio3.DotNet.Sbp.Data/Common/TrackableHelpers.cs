using Folio3.DotNet.Sbp.Data.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Folio3.DotNet.Sbp.Data.Common
{
    public static class TrackableHelpers
    {
        /// <summary>
        /// Walk the changes in a ChangeTracker over changes to TrackableEntity records
        /// and apply data to the fields
        /// </summary>
        /// <param name="changes">changes about to be saved</param>
        public static void PopulateTrackableFields(ChangeTracker changes)
        {
            foreach (var e in changes.Entries<TrackableEntity>())
            {
                TrackableEntity te = e.Entity;
                switch (e.State)
                {
                    case EntityState.Added:
                        break;

                    case EntityState.Modified:
                        te.Touch();
                        break;

                    default:
                    case EntityState.Deleted:
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                        break;
                }
            }
        }
    }
}
