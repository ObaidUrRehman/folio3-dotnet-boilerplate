using AutoMapper;
using Folio3.DotNet.Sbp.Service.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Folio3.DotNet.Sbp.Service.Common.Services
{
	public abstract class BaseService : DbContextService
	{
        public BaseService(
           DbContext context,
           ILogger logger,
           IMapper mapper)
            : base(context, logger, mapper)
        {
        }

        protected static ServiceResult<T> Success<T>(T data) =>
            Result(data: data, success: true);

        protected static ServiceResult<T> Failure<T>(string errorMessage) =>
            Result(data: default(T), success: false, errors: new List<string> { errorMessage });

        protected static ServiceResult<T> Result<T>(T data, bool success, List<string> errors = null) =>
            new ServiceResult<T>
            {
                Success = success,
                Data = data,
                Errors = errors ?? new List<string>()
            };
    }
}
