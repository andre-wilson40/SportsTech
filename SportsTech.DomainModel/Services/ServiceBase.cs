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
        protected IDbSet<TEntity> EntitySet { get;  private set; }

        protected ServiceBase(IDbSet<TEntity> entitySet)
        {
            EntitySet = entitySet;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await EntitySet.ToListAsync();
        }

        public virtual TEntity Add(TEntity ev)
        {
            return EntitySet.Add(ev);
        }

        public virtual void Remove(TEntity ev)
        {
            EntitySet.Remove(ev);
        }

        public virtual Task<TEntity> SingleAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression)
        {
            return EntitySet.SingleAsync(expression);
        }
    }
}
