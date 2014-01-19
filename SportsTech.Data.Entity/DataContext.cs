using Microsoft.AspNet.Identity.EntityFramework;
using SportsTech.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Entity
{
    public class DataContext : IdentityDbContext<ApplicationUser>, IDataContext
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
        public virtual IDbSet<Competition> Competitions { get; set; }
        
        public DataContext()
            : base("DataContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("User");
            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");

            modelBuilder.Entity<Club>()
                .HasMany(v => v.UserProfiles)
                .WithMany(v => v.Clubs)
                .Map(m =>
                {
                    m.MapLeftKey("ClubId");
                    m.MapRightKey("UserProfileId");
                    m.ToTable("UserProfileClub");
                });

            modelBuilder.Entity<Player>()
                .HasMany(v => v.Squads)
                .WithMany(v => v.Players)
                .Map(m =>
                {
                    m.MapLeftKey("PlayerId");
                    m.MapRightKey("SquadId");
                    m.ToTable("SquadMembers");
                });

            modelBuilder.Entity<Event>()
                .HasRequired(a => a.Participants)
                .WithMany()
                .HasForeignKey(a => a.ParticipantsId);
        }
    }
}
