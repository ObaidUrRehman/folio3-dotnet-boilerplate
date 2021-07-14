using Folio3.DotNet.Sbp.Service.Common.Dto;
using Folio3.DotNet.Sbp.Service.School.Dto;
using Folio3.DotNet.Sbp.Service.School.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Folio3.DotNet.Sbp.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StudentController : BaseController
	{
		public StudentService StudentService { get; }
		public StudentController(
			ILogger<StudentController> logger,
			StudentService studentService
			) : base(logger)
		{
			StudentService = studentService;
		}

		[HttpPost]
		public async Task<ResponseDto<StudentDto>> AddStudent([FromBody] StudentDto student)
		{
			return Result(await StudentService.AddStudentAsync(student));
		}

		[HttpPut("{id}")]
		public async Task<ResponseDto<StudentDto>> UpdateStudent(int id, [FromBody] StudentDto student)
		{
			return Result(await StudentService.UpdateStudentAsync(id, student));
		}
	}
}
