using System.Collections.Generic;
using System.Threading.Tasks;
using Folio3.Sbp.Service.Background;
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
    // [Authorize]
    public class StudentController : BaseController
    {
        public StudentController(ILogger<StudentController> logger, StudentService studentService) : base(logger)
        {
            StudentService = studentService;
        }

        public StudentService StudentService { get; }

        /// <summary>
        /// Gets all students as paged response
        /// </summary>
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ResponseDto<PagedResponseDto<StudentDto>>> GetStudents(int page = 0, int size = 100)
        {
            return Result(await StudentService.GetAllPaginatedDtoAsync(page, size));
        }

        /// <summary>
        /// Gets a student
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ResponseDto<StudentDto>> GetStudent(int id)
        {
            return Result(await StudentService.GetDtoAsync(id), true);
        }

        /// <summary>
        /// Adds a new student
        /// </summary>
        [HttpPost]
        public async Task<ResponseDto<StudentDto>> AddStudent([FromBody] StudentDto student)
        {
            return Result(await StudentService.AddDtoAsync(student));
        }

        /// <summary>
        /// Updates a student
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ResponseDto<StudentDto>> UpdateStudent(int id, [FromBody] StudentDto student)
        {
            return Result(await StudentService.UpdateDtoAsync(id, student));
        }

        /// <summary>
        /// Deletes a student
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ResponseDto<StudentDto>> DeleteStudent(int id)
        {
            return Result(await StudentService.DeleteDtoAsync(id));
        }

        /// <summary>
        /// Test endpoint to invoke the background tasks that inserts specified number of students
        /// </summary>
        [AllowAnonymous]
        [HttpPost("add-background")]
        public async Task<ResponseDto> Background([FromBody]int count, [FromServices] BackgroundJobManager backgroundJobManager)
        {
            await backgroundJobManager.QueueJobAsync(new List<int> {count});
            return Success();
        }
    }
}