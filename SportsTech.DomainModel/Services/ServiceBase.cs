using SportsTech.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services
{
    public abstract class ServiceBase<TEntity> : IService, IService<TEntity> where TEntity: IEntity 
    {
        protected IUnitOfWork UnitOfWork { get;  private set; }
        protected IRepository<TEntity> Repository { get; private set; }

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
            return await Repository.AsQueryable().ToListAsync();
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
            return Repository.SingleAsync(expression);
        }

        protected virtual Task<int> CountAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression)
        {
            return Repository.CountAsync(expression);
        }
    }
}
