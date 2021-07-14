using AutoMapper;
using Folio3.DotNet.Sbp.Data.School;
using Folio3.DotNet.Sbp.Data.School.Entities;
using Folio3.DotNet.Sbp.Service.Base;
using Folio3.DotNet.Sbp.Service.Common;
using Folio3.DotNet.Sbp.Service.Common.Services;
using Folio3.DotNet.Sbp.Service.School.Dto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Folio3.DotNet.Sbp.Service.School.Services
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
