using SportsTech.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services
{
    public abstract class ServiceBase<TEntity> : IService, IService<TEntity>, IWritableService<TEntity> where TEntity : IEntity 
    {
        protected IUnitOfWork UnitOfWork { get;  private set; }
        protected IRepository<TEntity> Repository { get; private set; }

        protected virtual IQueryable<TEntity> QueryAsync
        {
            get
            {
                return Repository.AsQueryable();
            }
        }

        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Repository = unitOfWork.GetRepository<TEntity>();
        }

        public virtual void SaveAnyChanges()
        {
            UnitOfWork.SaveAnyChanges();
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await QueryAsync.ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression)
        {
            return await QueryAsync.Where(expression).ToListAsync();
        }

        public ICollection<TEntity> GetAllOrderByAsync(System.Linq.Expressions.Expression<Func<TEntity, string>> keySelector)
        {
            return QueryAsync.OrderBy(keySelector).ToList();
        }

        public ICollection<TEntity> GetAllOrderByDescendingAsync(System.Linq.Expressions.Expression<Func<TEntity, string>> keySelector)
        {
            return QueryAsync.OrderByDescending(keySelector).ToList();
        }

        public virtual Task<bool> CanAdd(TEntity ev, IErrorHandler errorHandler)
        {
            return Task.Run(() => errorHandler.IsValid);
        }

        public virtual TEntity Add(TEntity ev)
        {
            return Repository.Add(ev);
        }

        public virtual void Remove(TEntity ev)
        {
            Repository.Remove(ev);
        }

        public virtual Task<TEntity> SingleAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression)
        {
            return QueryAsync.SingleAsync(expression);
        }

        public virtual Task<TEntity> SingleOrDefaultAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression)
        {
            return QueryAsync.SingleOrDefaultAsync(expression);
        }

        public virtual Task<bool> AnyAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression)
        {
            return QueryAsync.AnyAsync(expression);
        }

        public virtual Task<int> CountAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression)
        {
            return QueryAsync.CountAsync(expression);
        }
    }
}
