using System.Threading;
using System.Threading.Tasks;
using Folio3.Sbp.Data.AuditLogging;
using Folio3.Sbp.Data.Common;
using Folio3.Sbp.Data.Extensions;
using Folio3.Sbp.Data.School.Entities;
using Folio3.Sbp.Data.School.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Folio3.Sbp.Data.School
{
    // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/complex-data-model?view=aspnetcore-5.0
    public class SchoolDbContext : AuditedDbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options, AuditLogDbContext auditLogDbContext,
            ILogger<SchoolDbContext> logger, IAuditMetaData auditMetaData) : base(options, auditLogDbContext,
            logger, auditMetaData)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public virtual DbSet<CourseAssignment> CourseAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeleteEntity).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }

            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new {c.CourseID, c.InstructorID});
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await SaveChangesAsync(true, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            SoftDeleteHelper.ProcessSoftDelete(ChangeTracker);
            TrackableHelpers.PopulateTrackableFields(ChangeTracker);
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return result;
        }
    }
}