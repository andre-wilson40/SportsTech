namespace SportsTech.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventParticipantsScore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventParticipant", "ScoreFor", c => c.Int(nullable: false));
            AddColumn("dbo.EventParticipant", "ScoreAgainst", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventParticipant", "ScoreAgainst");
            DropColumn("dbo.EventParticipant", "ScoreFor");
        }
    }
}
