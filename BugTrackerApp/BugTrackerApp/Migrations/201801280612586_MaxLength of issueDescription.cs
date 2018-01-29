namespace BugTrackerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLengthofissueDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Issues", "IssueDescription", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Issues", "IssueDescription", c => c.String(nullable: false));
        }
    }
}
