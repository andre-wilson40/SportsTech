namespace SportsTech.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventParticipantsSingular : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventParticipant", "EventId", "dbo.Event");
            DropForeignKey("dbo.EventParticipant", "OppositionId", "dbo.Opposition");
            DropForeignKey("dbo.EventParticipant", "TeamId", "dbo.Team");
            DropIndex("dbo.EventParticipant", new[] { "EventId" });
            DropIndex("dbo.EventParticipant", new[] { "OppositionId" });
            DropIndex("dbo.EventParticipant", new[] { "TeamId" });
            AddColumn("dbo.Event", "ParticipantsId", c => c.Int(nullable: false));
            AlterColumn("dbo.EventParticipant", "TeamId", c => c.Int(nullable: false));
            AlterColumn("dbo.EventParticipant", "OppositionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Event", "ParticipantsId");
            CreateIndex("dbo.EventParticipant", "OppositionId");
            CreateIndex("dbo.EventParticipant", "TeamId");
            AddForeignKey("dbo.Event", "ParticipantsId", "dbo.EventParticipant", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventParticipant", "OppositionId", "dbo.Opposition", "Id", cascadeDelete: false);
            AddForeignKey("dbo.EventParticipant", "TeamId", "dbo.Team", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventParticipant", "TeamId", "dbo.Team");
            DropForeignKey("dbo.EventParticipant", "OppositionId", "dbo.Opposition");
            DropForeignKey("dbo.Event", "ParticipantsId", "dbo.EventParticipant");
            DropIndex("dbo.EventParticipant", new[] { "TeamId" });
            DropIndex("dbo.EventParticipant", new[] { "OppositionId" });
            DropIndex("dbo.Event", new[] { "ParticipantsId" });
            AlterColumn("dbo.EventParticipant", "OppositionId", c => c.Int());
            AlterColumn("dbo.EventParticipant", "TeamId", c => c.Int());
            DropColumn("dbo.Event", "ParticipantsId");
            CreateIndex("dbo.EventParticipant", "TeamId");
            CreateIndex("dbo.EventParticipant", "OppositionId");
            CreateIndex("dbo.EventParticipant", "EventId");
            AddForeignKey("dbo.EventParticipant", "TeamId", "dbo.Team", "Id");
            AddForeignKey("dbo.EventParticipant", "OppositionId", "dbo.Opposition", "Id");
            AddForeignKey("dbo.EventParticipant", "EventId", "dbo.Event", "Id", cascadeDelete: true);
        }
    }
}
