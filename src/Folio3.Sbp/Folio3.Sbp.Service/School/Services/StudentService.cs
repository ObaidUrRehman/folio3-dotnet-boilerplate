using AutoMapper;
using Folio3.Sbp.Data.School;
using Folio3.Sbp.Data.School.Entities;
using Folio3.Sbp.Service.Common.Services;
using Folio3.Sbp.Service.School.Dto;
using Microsoft.Extensions.Logging;

namespace Folio3.Sbp.Service.School.Services
{
    public class StudentService : BaseService<Student, StudentDto>
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
    }
}