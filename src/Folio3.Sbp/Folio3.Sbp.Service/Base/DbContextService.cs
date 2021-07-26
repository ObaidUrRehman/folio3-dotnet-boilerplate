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
    public abstract class DbContextService
    {
        public DbContextService(DbContext context, ILogger logger, IMapper mapper)
        {
            Context = context;
            Logger = logger;
            Mapper = mapper;
        }

        protected DbContext Context { get; }
        protected IMapper Mapper { get; }
        protected ILogger Logger { get; }

        private IQueryable<TEntity> GetEntityQuery<TEntity>() where TEntity : class, IBaseEntity
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public async Task<TDto> FindAsync<TDto, TEntity>(long id) where TEntity : class, IBaseEntity where TDto : IDto
        {
            return Mapper.Map<TDto>(await Context.Set<TEntity>().FindAsync(id));
        }

        public async Task<TDto> SingleOrDefaultAsync<TDto, TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, IBaseEntity where TDto : IDto
        {
            return Mapper.Map<TDto>(await GetEntityQuery<TEntity>().SingleOrDefaultAsync(predicate));
        }

        public virtual async Task<IList<TDto>> ToListAsync<TDto, TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, IBaseEntity
        {
            return Mapper.Map<List<TDto>>(await GetEntityQuery<TEntity>().Where(predicate).ToListAsync());
        }

        public async Task<PagedResponseDto<TDto>> GetPageAsync<TDto, TEntity>(int page, int size)
            where TEntity : class, IBaseEntity where TDto : IDto
        {
            return new PagedResponseDto<TDto>(
                Mapper.Map<IEnumerable<TDto>>(
                    await GetEntityQuery<TEntity>()
                        .Skip(page * size)
                        .Take(size)
                        .ToListAsync()),
                true,
                "",
                page,
                size,
                await GetEntityQuery<TEntity>().CountAsync());
        }

        public async Task<PagedResponseDto<TDto>> GetPageAsync<TDto, TEntity>(IQueryable<TEntity> query, int page,
            int size) where TEntity : class, IBaseEntity where TDto : IDto
        {
            return new PagedResponseDto<TDto>(
                Mapper.Map<IEnumerable<TDto>>(
                    await query
                        .Skip(page * size)
                        .Take(size)
                        .ToListAsync()),
                true,
                "",
                page,
                size,
                await query.CountAsync());
        }

        /// <summary>
        ///     Add an entity specified by the DTO, mapping it to the Entity and
        ///     inserting it into the correct table magically.
        /// </summary>
        public async Task<TDto> AddAsync<TDto, TEntity>(TDto dto) where TEntity : class, IBaseEntity where TDto : IDto
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
        public async Task<TDto> UpdateAsync<TDto, TEntity>(long id, TDto dto)
            where TEntity : class, IBaseEntity where TDto : IDto
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

        public async Task<TEntity> UpdateAsync<TEntity>(long id, TEntity entity) where TEntity : class, IBaseEntity
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
        public async Task<bool> DeleteAsync<TEntity>(long id) where TEntity : class, IBaseEntity
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