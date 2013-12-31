namespace SportsTech.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "EmailAddress", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.UserProfile", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfile", "UserName", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.UserProfile", "EmailAddress");
        }
    }
}
