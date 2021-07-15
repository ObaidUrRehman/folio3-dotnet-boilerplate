using System.Threading;
using System.Threading.Tasks;
using Folio3.DotNet.Sbp.Data.AuditLogging;
using Folio3.DotNet.Sbp.Data.Common;
using Folio3.DotNet.Sbp.Data.School.Entities;
using Folio3.DotNet.Sbp.Data.School.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Folio3.DotNet.Sbp.Data.School
{
    // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/complex-data-model?view=aspnetcore-5.0
    public class SchoolDbContext : AuditedDbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options, AuditLogDbContext auditLogDbContext,
            ILogger<SchoolDbContext> logger, AuditLogger auditLogger, IAuditMetaData auditMetaData) : base(options, auditLogDbContext,
            logger, auditLogger, auditMetaData)
        { }

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

            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new {c.CourseID, c.InstructorID});
        }

        public override int SaveChanges()
        {
            return SaveChanges(true);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return Task.Run(async () => await SaveChangesAsync(acceptAllChangesOnSuccess)).Result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await SaveChangesAsync(true, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            TrackableHelpers.PopulateTrackableFields(ChangeTracker);

            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            return result;
        }
    }
}