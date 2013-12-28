using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using SportsTech.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Collections.Generic;

namespace SportsTech.Domain.Tests
{
    public static class MockFactory
    {
        public static IDataContext GetEmptyContext()
        {
            var dataContext = new DataContext();
            
            return dataContext;
        }

        public static Mock<IDbSet<TEntity>> CreateDbSet<TEntity>(IList<TEntity> dbSet) where TEntity: IEntity 
        {
            var data = dbSet.AsQueryable();

            var mockSet = new Mock<IDbSet<TEntity>>();
            mockSet.As<IDbAsyncEnumerable<TEntity>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TEntity>(data.GetEnumerator()));
 
            mockSet.As<IQueryable<TEntity>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<TEntity>(data.Provider));
 
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            
            return mockSet;
        }

        internal class DataContext : IDataContext
        {
            public IDbSet<Data.Model.Event> Events { get; set; }

            public Database Database
            {
                get { throw new NotImplementedException(); }
            }

            public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
            {
                throw new NotImplementedException();
            }

            public int SaveChanges()
            {
                throw new NotImplementedException();
            }

            public DataContext()
            {
                Events = CreateDbSet(new List<Data.Model.Event>()).Object;
            }


            public IDbSet<Data.Model.Club> Clubs
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public IDbSet<Data.Model.Member> Members
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public IDbSet<Data.Model.Membership> Memberships
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public IDbSet<Data.Model.Season> Seasons
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public IDbSet<Data.Model.Team> Teams
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
