using Folio3.Sbp.Service.School.Dto;
using Folio3.Sbp.Service.School.Services;
using Folio3.Sbp.UnitTest.Mocks;
using Microsoft.Extensions.Logging;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Folio3.Sbp.UnitTest.ServiceTests.Student
{
	public class StudentServiceTests : IClassFixture<StudentServiceFixture>
	{
		private StudentServiceFixture Fixture { get; }
		private MockLogger<StudentService> Logger => Fixture.Logger;
		public ITestOutputHelper Output { get; }

		public StudentServiceTests(StudentServiceFixture fixture, ITestOutputHelper output)
		{
			Fixture = fixture;
			Logger.Output = Output = output;
		}

        [Fact]
        public async void Exercise_StudentService()
        {
            StudentService service = Fixture.CreateService();

            Logger.Log(LogLevel.Information, "Successfully created StudentService");

            var student = new StudentDto
            {
                FirstMidName = "Unit Test",
                LastName = "User 2",
                EnrollmentDate = DateTime.UtcNow
            };

            var result = await service.AddAsync(student);
            Assert.True(result.Success);
            Assert.True(result.Data.ID > 0);
        }
    }
}
