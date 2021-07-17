using System.Threading.Tasks;
using AutoMapper;
using Folio3.Sbp.Data.School;
using Folio3.Sbp.Data.School.Entities;
using Folio3.Sbp.Service.Common;
using Folio3.Sbp.Service.Common.Services;
using Folio3.Sbp.Service.School.Dto;
using Microsoft.Extensions.Logging;

namespace Folio3.Sbp.Service.School.Services
{
    public class StudentService : BaseService
    {
        public StudentService(
            SchoolDbContext context,
            ILogger<StudentService> logger,
            IMapper mapper)
            : base(context, logger, mapper)
        {
        }

        public async Task<ServiceResult<StudentDto>> AddStudentAsync(StudentDto student)
        {
            return Success(await AddAsync<StudentDto, Student>(student));
        }

        public async Task<ServiceResult<StudentDto>> UpdateStudentAsync(int id, StudentDto student)
        {
            return Success(await UpdateAsync<StudentDto, Student>(id, student));
        }
    }
}