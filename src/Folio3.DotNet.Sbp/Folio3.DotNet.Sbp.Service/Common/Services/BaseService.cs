using System.Collections.Generic;
using AutoMapper;
using Folio3.DotNet.Sbp.Service.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        protected static ServiceResult<T> Success<T>(T data)
        {
            return Result(data, true);
        }

        protected static ServiceResult<T> Failure<T>(string errorMessage)
        {
            return Result(default(T), false, new List<string> {errorMessage});
        }

        protected static ServiceResult<T> Result<T>(T data, bool success, List<string> errors = null)
        {
            return new ServiceResult<T>
            {
                Success = success,
                Data = data,
                Errors = errors ?? new List<string>()
            };
        }
    }
}