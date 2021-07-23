using Folio3.Sbp.Data.AuditLogging;
using Folio3.Sbp.Data.School;
using Folio3.Sbp.Data.School.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Folio3.Sbp.UnitTest.DataTests
{
	public class SchoolDbContextTests : IClassFixture<SchoolDbContextFixture>
    {
        private readonly SchoolDbContextFixture Fixture;

        private SchoolDbContext SchoolDbContext => Fixture.SchoolDbContext;
        private AuditLogDbContext AuditLogDbContext => Fixture.AuditLogDbContext;

        private long StudentId => 1;

        public SchoolDbContextTests(SchoolDbContextFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public async Task InitialChanges_FirstMidNameStudentRecord_IsPresent()
        {
            Assert.Equal(8, SchoolDbContext.Students.Count());

            var student = await SchoolDbContext.Students.AsQueryable().FirstOrDefaultAsync(s => s.ID == StudentId);

            Assert.Equal("Carson", student?.FirstMidName);
        }

        [Fact]
        public async Task SaveChangesAsync_AddAndDeleteStudent_HasAuditLogs()
        {
            Assert.Equal(8, SchoolDbContext.Students.Count());

            var student = new Student
            {
                FirstMidName = "Unit Test",
                LastName = "User",
                EnrollmentDate = DateTime.UtcNow
            };

            SchoolDbContext.Students.Add(student);
            await SchoolDbContext.SaveChangesAsync();

            Assert.Equal(9, SchoolDbContext.Students.Count());
            Assert.Equal(1, AuditLogDbContext.AuditLogs.Count());
            Assert.True(AuditLogDbContext.AuditLogChanges.Count() > 0);

            SchoolDbContext.Students.Remove(student);
            await SchoolDbContext.SaveChangesAsync();

            Assert.Equal(8, SchoolDbContext.Students.Count());
        }
    }
}
