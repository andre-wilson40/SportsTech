using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsTech.Data.Model;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SportsTech.Data
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

    public class DataContext: DbContext, IDataContext
    {
        public IDbSet<Event> Events { get; set; }
        public IDbSet<Club> Clubs { get; set; }
        public IDbSet<Member> Members { get; set; }
        public IDbSet<Membership> Memberships { get; set; }
        public IDbSet<Season> Seasons { get; set; }
        public IDbSet<Team> Teams { get; set; }

        public DataContext() : base("DataContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Customer>()
            //    .HasMany(v => v.UserProfiles)
            //    .WithMany(v => v.Customers)
            //    .Map(m =>
            //    {
            //        m.MapLeftKey("CustomerId");
            //        m.MapRightKey("UserProfileId");
            //        m.ToTable("CustomerManager");
            //    });
        }
    }
}
