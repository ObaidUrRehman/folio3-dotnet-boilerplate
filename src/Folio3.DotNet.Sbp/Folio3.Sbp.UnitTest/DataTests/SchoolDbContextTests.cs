using Folio3.Sbp.Data.AuditLogging;
using Folio3.Sbp.Data.School;
using Microsoft.EntityFrameworkCore;
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
    }
}
