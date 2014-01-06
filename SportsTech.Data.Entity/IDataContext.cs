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
        IDbSet<UserProfile> UserProfiles { get; set; }
        IDbSet<Club> Clubs { get; set; }
        IDbSet<Member> Members { get; set; }
        IDbSet<Membership> Memberships { get; set; }
        IDbSet<Team> Teams { get; set; }
        IDbSet<CompetitionRegistration> CompetitionRegistrations { get; set; }
        IDbSet<Squad> Squads { get; set; }
        IDbSet<Player> Players { get; set; }
        IDbSet<Season> Seasons { get; set; }        
        IDbSet<Event> Events { get; set; }
        IDbSet<EventParticipant> EventParticipants { get; set; }
        IDbSet<Teamsheet> Teamsheets { get; set; }
        IDbSet<SeasonRound> SeasonRounds { get; set; }
        IDbSet<EventTag> EventTags { get; set; }
        IDbSet<TagType> TagTypes { get; set; }
        IDbSet<Tag> Tags { get; set; }
        IDbSet<Stoppage> Stoppages { get; set; }
        IDbSet<Competition> Competitions { get; set; }
        
        System.Data.Entity.Database Database { get; }
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;        
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
