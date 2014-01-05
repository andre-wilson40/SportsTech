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
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);
        ICollection<TEntity> GetAllOrderByAsync(Expression<Func<TEntity, string>> keySelector);
        ICollection<TEntity> GetAllOrderByDescendingAsync(Expression<Func<TEntity, string>> keySelector);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression);
    }

    public interface IWritableService<TEntity> where TEntity : IEntity
    {
        Task<bool> CanAdd(TEntity ev, IErrorHandler errorHandler);
        TEntity Add(TEntity ev);
        void Remove(TEntity ev);
    }
}
