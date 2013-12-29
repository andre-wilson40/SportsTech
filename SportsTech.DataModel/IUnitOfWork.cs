using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data
{
    public interface IUnitOfWork
    {
        IRepository<TModel> GetRepository<TModel>() where TModel : class;
        int SaveAnyChanges();
    }
}
