using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Entity
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDataContext _dataContext;

        public UnitOfWork(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public virtual IRepository<TModel> GetRepository<TModel>() where TModel : class
        {
            return new DataRepository<TModel>(_dataContext);
        }

        public virtual int SaveAnyChanges()
        {
            return _dataContext.SaveChanges();
        }
    }

}
