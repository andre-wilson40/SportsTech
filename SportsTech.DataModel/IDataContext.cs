using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data
{
    public interface IDataContext
    {
        System.Data.Entity.Database Database { get; }
        DbEntityEntry<IEntity> Entry<IEntity>(IEntity entity) where IEntity : class;
        int SaveChanges();
    }
}
