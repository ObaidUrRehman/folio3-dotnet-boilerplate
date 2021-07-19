using System.Collections.Generic;
using System.Threading.Tasks;
using Folio3.Sbp.Service.Common.Dto;
using Folio3.Sbp.Service.School.Dto;
using Folio3.Sbp.Service.School.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Folio3.Sbp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class StudentController : BaseController
    {
        public StudentController(
            ILogger<StudentController> logger,
            StudentService studentService
        ) : base(logger)
        {
            StudentService = studentService;
        }

        public StudentService StudentService { get; }

        [HttpGet("all")]
        public async Task<ResponseDto<List<StudentDto>>> GetStudents()
        {
            return Result(await StudentService.GetStudentsAsync());
        }

        [HttpPost]
        public async Task<ResponseDto<StudentDto>> AddStudent([FromBody] StudentDto student)
        {
            return Result(await StudentService.AddStudentAsync(student));
        }

        [HttpPut("{id}")]
        public async Task<ResponseDto<StudentDto>> UpdateStudent(long id, [FromBody] StudentDto student)
        {
            return Result(await StudentService.UpdateStudentAsync(id, student));
        }

        [HttpDelete("{id}")]
        public async Task<ResponseDto<bool>> DeleteStudent(long id)
        {
            return Result(await StudentService.DeleteStudentAsync(id));
        }
    }
}