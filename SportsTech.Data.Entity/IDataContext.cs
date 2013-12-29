using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsTech.Data.Model;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SportsTech.Data.Entity
{
    public interface IDataContext
    {
        IDbSet<Event> Events { get; set; }
        IDbSet<Club> Clubs { get; set; }
        IDbSet<Member> Members { get; set; }
        IDbSet<Membership> Memberships { get; set; }
        IDbSet<Season> Seasons { get; set; }
        IDbSet<Team> Teams { get; set; }
        
        System.Data.Entity.Database Database { get; }
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;        
        int SaveChanges();
    }
}
