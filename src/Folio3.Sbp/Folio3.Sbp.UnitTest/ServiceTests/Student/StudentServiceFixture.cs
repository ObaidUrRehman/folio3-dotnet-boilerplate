using AutoMapper;
using Folio3.Sbp.Data.AuditLogging;
using Folio3.Sbp.Data.School;
using Folio3.Sbp.Service;
using Folio3.Sbp.Service.School.Services;
using Folio3.Sbp.UnitTest.Mocks;
using System;

namespace Folio3.Sbp.UnitTest.ServiceTests.Student
{
	public class StudentServiceFixture : IDisposable
	{
		public IMapper Mapper { get; }
		public MockLogger<StudentService> Logger { get; }
		public MockContext<SchoolDbContext> MockSchoolDbContext { get; set; }
		public MockContext<AuditLogDbContext> MockAuditLogDbContext { get; }

		public StudentServiceFixture()
		{
			var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
			Mapper = mappingConfig.CreateMapper();

			Logger = new MockLogger<StudentService>();

			MockAuditLogDbContext = new MockContext<AuditLogDbContext>(creator: options => new AuditLogDbContext(options));

			MockSchoolDbContext = new MockContext<SchoolDbContext>(creator: options => new SchoolDbContext(
				options: options,
				auditLogDbContext: MockAuditLogDbContext.Context,
				logger: new MockLogger<SchoolDbContext>(),
				auditMetaData: new MockAuditMetaData()));
		}

		public StudentService CreateService()
		{
			return new StudentService(MockSchoolDbContext.Context, Logger, Mapper);
		}

		public void Dispose()
		{
			MockSchoolDbContext.Dispose();
			MockAuditLogDbContext.Dispose();
		}
	}
}
