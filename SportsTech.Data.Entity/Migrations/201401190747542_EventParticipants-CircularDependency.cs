namespace SportsTech.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventParticipantsCircularDependency : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventParticipant", "EventId", "dbo.Event");
            DropIndex("dbo.EventParticipant", new[] { "EventId" });
            DropColumn("dbo.EventParticipant", "EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EventParticipant", "EventId", c => c.Int(nullable: false));
            CreateIndex("dbo.EventParticipant", "EventId");
            AddForeignKey("dbo.EventParticipant", "EventId", "dbo.Event", "Id", cascadeDelete: true);
        }
    }
}
