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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
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
                "dbo.Event",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeasonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Season", t => t.SeasonId, cascadeDelete: true)
                .Index(t => t.SeasonId);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClubId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .Index(t => t.ClubId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Team", "ClubId", "dbo.Club");
            DropForeignKey("dbo.Event", "SeasonId", "dbo.Season");
            DropForeignKey("dbo.Season", "ClubId", "dbo.Club");
            DropForeignKey("dbo.Member", "MembershipId", "dbo.Membership");
            DropForeignKey("dbo.Member", "ClubId", "dbo.Club");
            DropIndex("dbo.Team", new[] { "ClubId" });
            DropIndex("dbo.Event", new[] { "SeasonId" });
            DropIndex("dbo.Season", new[] { "ClubId" });
            DropIndex("dbo.Member", new[] { "MembershipId" });
            DropIndex("dbo.Member", new[] { "ClubId" });
            DropTable("dbo.Team");
            DropTable("dbo.Event");
            DropTable("dbo.Season");
            DropTable("dbo.Membership");
            DropTable("dbo.Member");
            DropTable("dbo.Club");
        }
    }
}
