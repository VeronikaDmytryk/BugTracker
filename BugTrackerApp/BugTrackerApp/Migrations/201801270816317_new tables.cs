namespace BugTrackerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentBody = c.String(),
                        CommentTime = c.DateTime(nullable: false),
                        IssueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Issues", t => t.IssueId, cascadeDelete: true)
                .Index(t => t.IssueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "IssueId", "dbo.Issues");
            DropIndex("dbo.Comments", new[] { "IssueId" });
            DropTable("dbo.Comments");
        }
    }
}
