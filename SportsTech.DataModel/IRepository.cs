using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data
{
    public interface IRepository<TModel>
    {
        TModel Add(TModel model);
        void Remove(TModel model);
        IQueryable<TModel> AsQueryable();      
        Task<TModel> SingleAsync(Expression<Func<TModel, bool>> expression);
        Task<TModel> SingleOrDefaultAsync(Expression<Func<TModel, bool>> expression);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<TModel, bool>> expression);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TModel, bool>> expression);
    }
}
