namespace SportsTech.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Competition : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Season", "ClubId", "dbo.Club");
            DropIndex("dbo.Season", new[] { "ClubId" });
            CreateTable(
                "dbo.Competition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ClubId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .Index(t => t.ClubId);
            
            AddColumn("dbo.Season", "CompetitionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Season", "CompetitionId");
            AddForeignKey("dbo.Season", "CompetitionId", "dbo.Competition", "Id", cascadeDelete: true);
            DropColumn("dbo.Season", "ClubId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Season", "ClubId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Season", "CompetitionId", "dbo.Competition");
            DropForeignKey("dbo.Competition", "ClubId", "dbo.Club");
            DropIndex("dbo.Season", new[] { "CompetitionId" });
            DropIndex("dbo.Competition", new[] { "ClubId" });
            DropColumn("dbo.Season", "CompetitionId");
            DropTable("dbo.Competition");
            CreateIndex("dbo.Season", "ClubId");
            AddForeignKey("dbo.Season", "ClubId", "dbo.Club", "Id", cascadeDelete: true);
        }
    }
}
