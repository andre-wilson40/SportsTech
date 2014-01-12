namespace SportsTech.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompetitionRegistrationLink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamSeason", "Team_Id", "dbo.Team");
            DropForeignKey("dbo.TeamSeason", "Season_Id", "dbo.Season");
            DropIndex("dbo.TeamSeason", new[] { "Team_Id" });
            DropIndex("dbo.TeamSeason", new[] { "Season_Id" });
            DropTable("dbo.TeamSeason");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeamSeason",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Season_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Season_Id });
            
            CreateIndex("dbo.TeamSeason", "Season_Id");
            CreateIndex("dbo.TeamSeason", "Team_Id");
            AddForeignKey("dbo.TeamSeason", "Season_Id", "dbo.Season", "Id");
            AddForeignKey("dbo.TeamSeason", "Team_Id", "dbo.Team", "Id");
        }
    }
}
