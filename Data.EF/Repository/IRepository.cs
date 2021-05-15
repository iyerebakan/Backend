using Domain;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        IEnumerable<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int skip = 0, int take = 20);

        Task<IEnumerable<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int skip = 0, int take = 20);

        IEnumerable<TResult> GetList<TResult>(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Expression<Func<TEntity, TResult>> selector = null);

        Task<IEnumerable<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Expression<Func<TEntity, TResult>> selector = null);

        IEnumerable<TResult> GetPagedList<TResult>(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Expression<Func<TEntity, TResult>> selector = null,
            int skip = 0, int take = 20);

        Task<IEnumerable<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Expression<Func<TEntity, TResult>> selector = null,
            int skip = 0, int take = 20);

        TEntity Get(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        TResult Get<TResult>(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Expression<Func<TEntity, TResult>> selector = null);

        Task<TResult> GetAsync<TResult>(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Expression<Func<TEntity, TResult>> selector = null);

        int Count(Expression<Func<TEntity, bool>> predicate = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);


        // Db Insert
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Add(List<TEntity> entities);
        Task AddAsync(List<TEntity> entities);

        //Db Update
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Update(List<TEntity> entities);
        Task UpdateAsync(List<TEntity> entities);

        // Db Delete
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void Delete(List<TEntity> entities);
        Task DeleteAsync(List<TEntity> entities);
    }
}
