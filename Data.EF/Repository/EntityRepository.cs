using Data.EF.Context;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Repository
{
    public partial class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContextBase dbContext;
        protected DbSet<TEntity> DbSet { get; }
        public EntityRepository(DbContextBase dbContext)
        {
            this.dbContext = dbContext;
            this.DbSet = this.dbContext.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public void Add(List<TEntity> entities)
        {
            this.DbSet.AddRange(entities);
        }

        public Task AddAsync(TEntity entity)
        {
            return this.DbSet.AddAsync(entity).AsTask();
        }

        public Task AddAsync(List<TEntity> entities)
        {
            return this.DbSet.AddRangeAsync(entities);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return this.DbSet.Count(predicate);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return this.DbSet.CountAsync(predicate);
        }

        public void Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }

        public void Delete(List<TEntity> entities)
        {
            this.DbSet.RemoveRange(entities);
        }

        public Task DeleteAsync(TEntity entity)
        {
            this.Delete(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(List<TEntity> entities)
        {
            this.Delete(entities);
            return Task.CompletedTask;
        }

        public void Update(TEntity entity)
        {
            this.DbSet.Update(entity);
        }

        public void Update(List<TEntity> entities)
        {
            this.DbSet.UpdateRange(entities);
        }

        public Task UpdateAsync(TEntity entity)
        {
            this.Update(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(List<TEntity> entities)
        {
            this.UpdateAsync(entities);
            return Task.CompletedTask;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = this.CreateQuery(predicate: predicate, include: include);
            return query.SingleOrDefault<TEntity>();
        }

        public TResult Get<TResult>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Expression<Func<TEntity, TResult>> selector = null)
        {
            var query = this.CreateQuery(predicate: predicate, include: include);
            return query.Select(selector).SingleOrDefault();
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = this.CreateQuery(predicate: predicate, include: include);
            return query.SingleOrDefaultAsync<TEntity>();
        }

        public Task<TResult> GetAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Expression<Func<TEntity, TResult>> selector = null)
        {
            var query = this.CreateQuery(predicate: predicate, include: include);
            return query.Select(selector).SingleOrDefaultAsync();
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = this.CreateQuery(predicate, orderBy, include);
            return query.ToList();
        }

        public IEnumerable<TResult> GetList<TResult>(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Expression<Func<TEntity, TResult>> selector = null)
        {
            var query = this.CreateQuery(predicate, orderBy, include);
            return query.Select(selector).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = this.CreateQuery(predicate, orderBy, include);
            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Expression<Func<TEntity, TResult>> selector = null)
        {
            var query = this.CreateQuery(predicate, orderBy, include);
            return await query.Select(selector).ToListAsync().ConfigureAwait(false);
        }

        public IEnumerable<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int skip = 0, int take = 20)
        {
            var query = this.CreateQuery(predicate, orderBy, include);
            return query.Skip(skip).Take(take).ToList();
        }

        public IEnumerable<TResult> GetPagedList<TResult>(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Expression<Func<TEntity, TResult>> selector = null, int skip = 0, int take = 20)
        {
            var query = this.CreateQuery(predicate, orderBy, include);
            return query.Skip(skip).Take(take).Select(selector).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int skip = 0, int take = 20)
        {
            var query = this.CreateQuery(predicate, orderBy, include);
            return await query.Skip(skip).Take(take).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Expression<Func<TEntity, TResult>> selector = null, int skip = 0, int take = 20)
        {
            var query = this.CreateQuery(predicate, orderBy, include);
            return await query.Skip(skip).Take(take).Select(selector).ToListAsync().ConfigureAwait(false);
        }


        protected IQueryable<TEntity> CreateQuery(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = this.DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }
    }
}
