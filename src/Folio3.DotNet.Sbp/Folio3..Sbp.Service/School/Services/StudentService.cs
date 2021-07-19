using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Folio3.Sbp.Data.School;
using Folio3.Sbp.Data.School.Entities;
using Folio3.Sbp.Service.Common;
using Folio3.Sbp.Service.Common.Services;
using Folio3.Sbp.Service.School.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Folio3.Sbp.Service.School.Services
{
    public class StudentService : BaseService
    {
        private SchoolDbContext SchoolDbContext { get; }
        public StudentService(
            SchoolDbContext context,
            ILogger<StudentService> logger,
            IMapper mapper)
            : base(context, logger, mapper)
        {
            SchoolDbContext = context;
        }

        private IQueryable<Student> Students => SchoolDbContext.Students.AsQueryable();

        public async Task<ServiceResult<StudentDto>> AddStudentAsync(StudentDto student)
        {
            return Success(await AddAsync<StudentDto, Student>(student));
        }

        public async Task<ServiceResult<StudentDto>> UpdateStudentAsync(long id, StudentDto student)
        {
            return Success(await UpdateAsync<StudentDto, Student>(id, student));
        }

        public async Task<ServiceResult<bool>> DeleteStudentAsync(long id)
        {
            return Success(await DeleteAsync<Student>(id));
        }

        public async Task<ServiceResult<List<StudentDto>>> GetStudentsAsync()
        {
            return Success(Mapper.Map<List<StudentDto>>(await Students.ToListAsync()));
        }
    }
}