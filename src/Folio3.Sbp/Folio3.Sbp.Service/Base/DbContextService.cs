using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Folio3.Sbp.Data.Common;
using Folio3.Sbp.Service.Common.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Folio3.Sbp.Service.Base
{
    /// <summary>
    /// A base service class that has some niceties of the GenericService, but also
    /// allows it to handle multiple database tables and does away with a lot of the
    /// convoluted and annoying code
    /// </summary>
    public abstract class DbContextService<TEntity, TDto>
        where TEntity : class, IBaseEntity
        where TDto : IDto 
        
    {
        protected DbContextService(DbContext context, ILogger logger, IMapper mapper)
        {
            Context = context;
            Logger = logger;
            Mapper = mapper;
        }

        protected DbContext Context { get; }
        protected IMapper Mapper { get; }
        protected ILogger Logger { get; }

        protected IQueryable<TEntity> GetEntityQuery()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        protected async Task<TDto> FindDtoAsync(long id)
        {
            return Mapper.Map<TDto>(await Context.Set<TEntity>().FindAsync(id));
        }

        protected async Task<TDto> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Mapper.Map<TDto>(await GetEntityQuery().SingleOrDefaultAsync(predicate));
        }

        protected virtual async Task<IList<TDto>> ToListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Mapper.Map<List<TDto>>(await GetEntityQuery().Where(predicate).ToListAsync());
        }

        protected async Task<PagedResponseDto<TDto>> GetAllPaginatedDtoAsync(int page, int size)
        {
            return new PagedResponseDto<TDto>(
                Mapper.Map<IEnumerable<TDto>>(
                    await GetEntityQuery()
                        .Skip(page * size)
                        .Take(size)
                        .ToListAsync()),
                page,
                size,
                await GetEntityQuery().CountAsync());
        }

        protected async Task<PagedResponseDto<TDto>> GetPageAsync(IQueryable<TEntity> query, int page, int size)
        {
            return new PagedResponseDto<TDto>(
                Mapper.Map<IEnumerable<TDto>>(
                    await query
                        .Skip(page * size)
                        .Take(size)
                        .ToListAsync()),
                page,
                size,
                await query.CountAsync());
        }

        /// <summary>
        ///     Add an entity specified by the DTO, mapping it to the Entity and
        ///     inserting it into the correct table magically.
        /// </summary>
        protected async Task<TDto> AddDtoAsync(TDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var entity = Mapper.Map<TEntity>(dto);
            //entity.Initialize();

            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<TDto>(entity);
        }

        /// <summary>
        /// Update an entity based on an ID and an incoming DTO which gets mapped to the entity
        /// </summary>
        protected async Task<TDto> UpdateDtoAsync(long id, TDto dto)
        {
            var existing = await Context.Set<TEntity>().FindAsync(id);
            if (existing == null)
                throw new ArgumentException("Invalid Id");

            var entity = Mapper.Map(dto, existing);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            //entity.Touch();

            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
            return Mapper.Map<TDto>(entity);
        }

        protected async Task<TEntity> UpdateAsync(long id, TEntity entity)
        {
            var existing = await Context.Set<TEntity>().FindAsync(id);
            if (existing == null)
                throw new ArgumentException("Invalid Id");

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            //entity.Touch();

            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Delete an entity based on the ID
        /// </summary>
        protected async Task<bool> DeleteEntityAsync(long id)
        {
            var existing = await Context.Set<TEntity>().FindAsync(id);
            if (existing == null)
                throw new ArgumentException("Invalid Id");

            Context.Set<TEntity>().Remove(existing);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}