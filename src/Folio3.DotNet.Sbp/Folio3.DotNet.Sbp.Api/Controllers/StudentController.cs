using System.Threading.Tasks;
using Folio3.DotNet.Sbp.Service.Common.Dto;
using Folio3.DotNet.Sbp.Service.School.Dto;
using Folio3.DotNet.Sbp.Service.School.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Folio3.DotNet.Sbp.Api.Controllers
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