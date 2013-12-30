namespace SportsTech.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Club",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(nullable: false),
                        ClubId = c.Int(nullable: false),
                        MembershipId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .ForeignKey("dbo.Membership", t => t.MembershipId, cascadeDelete: true)
                .Index(t => t.ClubId)
                .Index(t => t.MembershipId);
            
            CreateTable(
                "dbo.Membership",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ValidFrom = c.DateTime(nullable: false),
                        ValidForDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Season",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ClubId = c.Int(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .Index(t => t.ClubId);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClubId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .Index(t => t.ClubId);
            
            CreateTable(
                "dbo.CompetitionRegistration",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        SeasonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Season", t => t.SeasonId, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.TeamId, cascadeDelete: false)
                .Index(t => t.SeasonId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 200),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        TimeZone = c.String(nullable: false, maxLength: 50),
                        DateFormat = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventParticipant",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Event", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeasonId = c.Int(nullable: false),
                        SeasonRoundId = c.Int(nullable: false),
                        EventDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SeasonRound", t => t.SeasonRoundId, cascadeDelete: true)
                .ForeignKey("dbo.Season", t => t.SeasonId, cascadeDelete: true)
                .Index(t => t.SeasonRoundId)
                .Index(t => t.SeasonId);
            
            CreateTable(
                "dbo.SeasonRound",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stoppage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoppedAfterSeconds = c.Int(nullable: false),
                        StoppedForSeconds = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Event", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.EventTag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventParticipantId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                        RecordedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EventParticipant", t => t.EventParticipantId, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagId, cascadeDelete: true)
                .Index(t => t.EventParticipantId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        ClubId = c.Int(),
                        TagTypeId = c.Int(nullable: false),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Club", t => t.ClubId)
                .ForeignKey("dbo.Tag", t => t.ParentId)
                .ForeignKey("dbo.TagType", t => t.TagTypeId, cascadeDelete: true)
                .Index(t => t.ClubId)
                .Index(t => t.ParentId)
                .Index(t => t.TagTypeId);
            
            CreateTable(
                "dbo.TagType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teamsheet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventParticipantId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        PositionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EventParticipant", t => t.EventParticipantId, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Position", t => t.PositionId, cascadeDelete: true)
                .Index(t => t.EventParticipantId)
                .Index(t => t.PlayerId)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Squad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CompetitionRegistrationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompetitionRegistration", t => t.CompetitionRegistrationId, cascadeDelete: true)
                .Index(t => t.CompetitionRegistrationId);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamSeason",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Season_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Season_Id })
                .ForeignKey("dbo.Team", t => t.Team_Id)
                .ForeignKey("dbo.Season", t => t.Season_Id)
                .Index(t => t.Team_Id)
                .Index(t => t.Season_Id);
            
            CreateTable(
                "dbo.UserClubAffliations",
                c => new
                    {
                        ClubId = c.Int(nullable: false),
                        UserProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClubId, t.UserProfileId })
                .ForeignKey("dbo.Club", t => t.ClubId)
                .ForeignKey("dbo.UserProfile", t => t.UserProfileId)
                .Index(t => t.ClubId)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.SquadMembers",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        SquadId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.SquadId })
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .ForeignKey("dbo.Squad", t => t.SquadId)
                .Index(t => t.PlayerId)
                .Index(t => t.SquadId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teamsheet", "PositionId", "dbo.Position");
            DropForeignKey("dbo.Teamsheet", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.SquadMembers", "SquadId", "dbo.Squad");
            DropForeignKey("dbo.SquadMembers", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Squad", "CompetitionRegistrationId", "dbo.CompetitionRegistration");
            DropForeignKey("dbo.Teamsheet", "EventParticipantId", "dbo.EventParticipant");
            DropForeignKey("dbo.Tag", "TagTypeId", "dbo.TagType");
            DropForeignKey("dbo.Tag", "ParentId", "dbo.Tag");
            DropForeignKey("dbo.EventTag", "TagId", "dbo.Tag");
            DropForeignKey("dbo.Tag", "ClubId", "dbo.Club");
            DropForeignKey("dbo.EventTag", "EventParticipantId", "dbo.EventParticipant");
            DropForeignKey("dbo.Stoppage", "EventId", "dbo.Event");
            DropForeignKey("dbo.Event", "SeasonId", "dbo.Season");
            DropForeignKey("dbo.Event", "SeasonRoundId", "dbo.SeasonRound");
            DropForeignKey("dbo.EventParticipant", "EventId", "dbo.Event");
            DropForeignKey("dbo.UserClubAffliations", "UserProfileId", "dbo.UserProfile");
            DropForeignKey("dbo.UserClubAffliations", "ClubId", "dbo.Club");
            DropForeignKey("dbo.TeamSeason", "Season_Id", "dbo.Season");
            DropForeignKey("dbo.TeamSeason", "Team_Id", "dbo.Team");
            DropForeignKey("dbo.CompetitionRegistration", "TeamId", "dbo.Team");
            DropForeignKey("dbo.CompetitionRegistration", "SeasonId", "dbo.Season");
            DropForeignKey("dbo.Team", "ClubId", "dbo.Club");
            DropForeignKey("dbo.Season", "ClubId", "dbo.Club");
            DropForeignKey("dbo.Member", "MembershipId", "dbo.Membership");
            DropForeignKey("dbo.Member", "ClubId", "dbo.Club");
            DropIndex("dbo.Teamsheet", new[] { "PositionId" });
            DropIndex("dbo.Teamsheet", new[] { "PlayerId" });
            DropIndex("dbo.SquadMembers", new[] { "SquadId" });
            DropIndex("dbo.SquadMembers", new[] { "PlayerId" });
            DropIndex("dbo.Squad", new[] { "CompetitionRegistrationId" });
            DropIndex("dbo.Teamsheet", new[] { "EventParticipantId" });
            DropIndex("dbo.Tag", new[] { "TagTypeId" });
            DropIndex("dbo.Tag", new[] { "ParentId" });
            DropIndex("dbo.EventTag", new[] { "TagId" });
            DropIndex("dbo.Tag", new[] { "ClubId" });
            DropIndex("dbo.EventTag", new[] { "EventParticipantId" });
            DropIndex("dbo.Stoppage", new[] { "EventId" });
            DropIndex("dbo.Event", new[] { "SeasonId" });
            DropIndex("dbo.Event", new[] { "SeasonRoundId" });
            DropIndex("dbo.EventParticipant", new[] { "EventId" });
            DropIndex("dbo.UserClubAffliations", new[] { "UserProfileId" });
            DropIndex("dbo.UserClubAffliations", new[] { "ClubId" });
            DropIndex("dbo.TeamSeason", new[] { "Season_Id" });
            DropIndex("dbo.TeamSeason", new[] { "Team_Id" });
            DropIndex("dbo.CompetitionRegistration", new[] { "TeamId" });
            DropIndex("dbo.CompetitionRegistration", new[] { "SeasonId" });
            DropIndex("dbo.Team", new[] { "ClubId" });
            DropIndex("dbo.Season", new[] { "ClubId" });
            DropIndex("dbo.Member", new[] { "MembershipId" });
            DropIndex("dbo.Member", new[] { "ClubId" });
            DropTable("dbo.SquadMembers");
            DropTable("dbo.UserClubAffliations");
            DropTable("dbo.TeamSeason");
            DropTable("dbo.Position");
            DropTable("dbo.Squad");
            DropTable("dbo.Player");
            DropTable("dbo.Teamsheet");
            DropTable("dbo.TagType");
            DropTable("dbo.Tag");
            DropTable("dbo.EventTag");
            DropTable("dbo.Stoppage");
            DropTable("dbo.SeasonRound");
            DropTable("dbo.Event");
            DropTable("dbo.EventParticipant");
            DropTable("dbo.UserProfile");
            DropTable("dbo.CompetitionRegistration");
            DropTable("dbo.Team");
            DropTable("dbo.Season");
            DropTable("dbo.Membership");
            DropTable("dbo.Member");
            DropTable("dbo.Club");
        }
    }
}
