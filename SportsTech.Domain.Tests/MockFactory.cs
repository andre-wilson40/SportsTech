using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using SportsTech.Data;
using SportsTech.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using SportsTech.Data.Model;
using SportsTech.Domain.Services;
using Moq.Language.Flow;
using Moq.Language;
using System.Threading.Tasks;

namespace SportsTech.Domain.Tests
{
    public static class MockFactory
    {
        public static IDataContext GetEmptyContext()
        {
            return new DataContext();
        }

        public static UserProfile GetUserProfile()
        {
            return new UserProfile
                {
                    Id = 1,
                    FirstName = "Andre",
                    LastName = "Wilson",
                    EmailAddress = "andre.wilson40@gmail.com",
                    TimeZone = "en-NZ"
                };
        }

        public static Mock<IUserService> GetUserService(IUnitOfWork uow)
        {            
            var clubRepository = uow.GetRepository<Data.Model.Club>();

            var userProfile = GetUserProfile();
            userProfile.Clubs = clubRepository.AsQueryable().ToList() ;

            var mockService = new Mock<IUserService>();
            mockService
                .Setup(m => m.CurrentUserProfile())
                .Returns(userProfile);

            return mockService;
        }

        public static IUnitOfWork GetUnitOfWork(IDataContext dataContext)
        {
            return new UnitOfWork(dataContext);
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

        public static IReturnsResult<TMock> ReturnsAsync<TMock, TResult>(
            this IReturns<TMock, Task<TResult>> setup, TResult value)
            where TMock : class
        {
            return setup.Returns(Task.FromResult(value));
        }
        
        internal class DataContext : IDataContext
        {
            public virtual IDbSet<UserProfile> UserProfiles { get; set; }
            public virtual IDbSet<Club> Clubs { get; set; }
            public virtual IDbSet<Member> Members { get; set; }
            public virtual IDbSet<Event> Events { get; set; }
            public virtual IDbSet<Membership> Memberships { get; set; }
            public virtual IDbSet<Season> Seasons { get; set; }
            public virtual IDbSet<Team> Teams { get; set; }
            public virtual IDbSet<CompetitionRegistration> CompetitionRegistrations { get; set; }
            public virtual IDbSet<Squad> Squads { get; set; }
            public virtual IDbSet<Player> Players { get; set; }
            public virtual IDbSet<EventParticipant> EventParticipants { get; set; }
            public virtual IDbSet<Teamsheet> Teamsheets { get; set; }
            public virtual IDbSet<SeasonRound> SeasonRounds { get; set; }
            public virtual IDbSet<EventTag> EventTags { get; set; }
            public virtual IDbSet<TagType> TagTypes { get; set; }
            public virtual IDbSet<Tag> Tags { get; set; }
            public virtual IDbSet<Stoppage> Stoppages { get; set; }

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


            public Task<int> SaveChangesAsync()
            {
                throw new NotImplementedException();
            }
        }
    }
}
