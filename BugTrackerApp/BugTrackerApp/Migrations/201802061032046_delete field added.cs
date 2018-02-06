namespace BugTrackerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletefieldadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Issues", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projects", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Deleted");
            DropColumn("dbo.Issues", "Deleted");
            DropColumn("dbo.Comments", "Deleted");
        }
    }
}
