using SportsTech.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services
{
    public interface IService
    {
        void SaveAnyChanges();   
    }

    public interface IService<TEntity> where TEntity : IEntity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression);
    }

    public interface IWritableService<TEntity> where TEntity : IEntity
    {
        bool CanAdd(TEntity ev, IErrorHandler errorHandler);
        TEntity Add(TEntity ev);
        void Remove(TEntity ev);
    }
}
