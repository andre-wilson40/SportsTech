namespace SportsTech.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Opposition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Opposition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EventParticipant", "TeamId", c => c.Int());
            AddColumn("dbo.EventParticipant", "OppositionId", c => c.Int());
            AddColumn("dbo.EventParticipant", "IsHomeGame", c => c.Boolean(nullable: false));
            CreateIndex("dbo.EventParticipant", "OppositionId");
            CreateIndex("dbo.EventParticipant", "TeamId");
            AddForeignKey("dbo.EventParticipant", "OppositionId", "dbo.Opposition", "Id");
            AddForeignKey("dbo.EventParticipant", "TeamId", "dbo.Team", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventParticipant", "TeamId", "dbo.Team");
            DropForeignKey("dbo.EventParticipant", "OppositionId", "dbo.Opposition");
            DropIndex("dbo.EventParticipant", new[] { "TeamId" });
            DropIndex("dbo.EventParticipant", new[] { "OppositionId" });
            DropColumn("dbo.EventParticipant", "IsHomeGame");
            DropColumn("dbo.EventParticipant", "OppositionId");
            DropColumn("dbo.EventParticipant", "TeamId");
            DropTable("dbo.Opposition");
        }
    }
}
