using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Folio3.Sbp.Data.Common;
using Folio3.Sbp.Service.Base;
using Folio3.Sbp.Service.Common.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Folio3.Sbp.Service.Common.Services
{
    public abstract class BaseService <TEntity, TDto> : DbContextService <TEntity, TDto> 
        where TEntity : class, IBaseEntity 
        where TDto : class, IDto
    {
        protected BaseService(
            DbContext context,
            ILogger logger,
            IMapper mapper)
            : base(context, logger, mapper)
        {
        }

        public async Task<ServiceResult<TDto>> AddAsync(TDto dto)
        {
            return Success(await AddDtoAsync(dto));
        }

        public async Task<ServiceResult<TDto>> UpdateAsync(int id, TDto dto)
        {
            return Success(await UpdateDtoAsync(id, dto));
        }

        public async Task<ServiceResult<TDto>> DeleteAsync(int id)
        {
            bool deleted = await DeleteEntityAsync(id);
            return Result(default(TDto), deleted);
        }

        public async Task<ServiceResult<TDto>> GetAsync(int id)
        {
            return Success(await FindDtoAsync(id));
        }

        public async Task<ServiceResult<PagedResponseDto<TDto>>> GetPageAsync(int page, int size)
        {
            return Success(await GetAllPaginatedDtoAsync(page, size));
        }


        protected static ServiceResult<TDto> Success(TDto data)
        {
            return Result(data, true);
        }

        protected static ServiceResult<PagedResponseDto<TDto>> Success(PagedResponseDto<TDto> data)
        {
            return Result(data, true);
        }

        protected static ServiceResult<TDto> Failure(string errorMessage)
        {
            return Result(default(TDto), false, new List<string> {errorMessage});
        }

        protected static ServiceResult<TDto> Failure(List<string> errorMessages)
        {
            return Result(default(TDto), false, errorMessages);
        }

        protected static ServiceResult<TDto> Result(TDto data, bool success, List<string> errors = null)
        {
            return new ServiceResult<TDto>
            {
                Success = success,
                Data = data,
                Errors = errors ?? new List<string>()
            };
        }

        protected static ServiceResult<PagedResponseDto<TDto>> Result(PagedResponseDto<TDto> data, bool success, List<string> errors = null)
        {
            return new ServiceResult<PagedResponseDto<TDto>>
            {
                Success = success,
                Data = data,
                Errors = errors ?? new List<string>()
            };
        }
    }
}